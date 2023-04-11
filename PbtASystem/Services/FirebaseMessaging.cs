using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PbtASystem.PbtASupport;
using Blazored.LocalStorage;
using System;
using MQTTnet.Client;
using MQTTnet;
using System.Text.Json.Serialization;

namespace PbtASystem.Services;

public class FirebaseMessaging
{
    public event EventHandler<MQTTMessage> MessageReceived;
    public FirebaseMessaging(IToastService _toaster, FirebaseData _data)
    {
        Toaster = _toaster;
        Data = _data;
    }

	private Guid ClientID;
    private string ClientName;
    private string ClientIDString => ClientID.ToString();   
    string baseTopic;
    IMqttClient? mqttClient;
    IToastService Toaster;
    FirebaseData Data;
    public bool isConnected => mqttClient?.IsConnected ?? false;
    private bool AlreadyConnecting = false;

    public async Task StartMessaging()
    {
        if(AlreadyConnecting) return;

        if (await Data.IsDefaultCharacterIDSet())
        {
            AlreadyConnecting = true;
            ClientID = await Data.GetDefaultCharacterID();
            ClientName = Data.GetCharacterByID(ClientID).Name;
            baseTopic = "VikingBoardGames";
            await Init();
            AlreadyConnecting = false;
        }
        else
            Toaster.ShowError("Cannot connect messaging because Player is not selected");
    }

    public async Task CheckIfReConnnectionNeeded()
    {
        if(isConnected) { 
            return;
        } else {
            await StartMessaging();
        }
    }


	public async Task Init()
    {
        await Connect_Client();
        await SubscriteTopic(baseTopic);
    }

    public async Task Connect_Client()
    {

        var mqttFactory = new MqttFactory();

        mqttClient = mqttFactory.CreateMqttClient();


        if (mqttClient != null)
        {
            mqttClient.ApplicationMessageReceivedAsync -= ProcessIncomingMessage;
            mqttClient.ApplicationMessageReceivedAsync += ProcessIncomingMessage;
        }

        if (mqttClient != null)
        {
            try
            {

                var mqttClientOptions = new MqttClientOptionsBuilder()
               .WithClientId(ClientIDString)
               .WithWebSocketServer("broker.hivemq.com:8884/mqtt")
               .WithTls()
               .Build();

                var result = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var ic = mqttClient.IsConnected;
                await Task.Delay(50);
                if (ic) { Toaster.ShowSuccess("Connected to MQTT"); }
                else { Toaster.ShowError("Could not connect to MQTT"); }

            }
            catch (Exception e)
            {
                Toaster.ShowError($"Error: {e}");
            }
        }
        else
            throw new Exception("Cannot create mqttClient");

        Console.WriteLine("The MQTT client is connected.");
    }

    public async Task SubscriteTopic(string topic)
    {
        if (isConnected)
        {
			var mqttFactory = new MqttFactory();
			var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
				.WithTopicFilter(
					f =>
					{
						f.WithTopic(topic);
					})
				.Build();

			if (mqttClient is not null)
				await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
		}       
    }

    private async Task ProcessIncomingMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        string incomingMessage = System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload);
        var encodedMessage = System.Text.Json.JsonSerializer.Deserialize<MQTTMessage>(incomingMessage);
        Toaster.ShowInfo(encodedMessage.SenderName + ": "+ encodedMessage.Message);
        MessageReceived?.Invoke(this, encodedMessage);
    }

    public async Task SendMessage(MQTTMessage msg)
    {
        msg.sender = ClientID;
        msg.SenderName = ClientName;    

        if (mqttClient is null)
        {
            Toaster.ShowError("mqttclient is null");
            return;
        }

        if (!mqttClient.IsConnected)
            await Connect_Client();

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(baseTopic)
            .WithPayload(System.Text.Json.JsonSerializer.Serialize(msg))
            .Build();

        if (mqttClient != null)
            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
    }

    public async Task SendTestMessage()
    {
        await SendMessage(new CommMessage { Message = "This is a test", Kind = MessageKind.SimpleComm });
    }
}

public enum MessageKind { NotSet, SimpleComm, MoveRoll }

[JsonDerivedType(typeof(MQTTMessage), typeDiscriminator: "base")]
[JsonDerivedType(typeof(CommMessage), typeDiscriminator: "comm")]
[JsonDerivedType(typeof(RollMessage), typeDiscriminator: "roll")]
public class MQTTMessage
{
    public Guid sender { get; set; } = new Guid();
    public string SenderName { get; set; } = "";
    public MessageKind Kind { get; set; }
    public string Message { get; set; } = "";
}

public class CommMessage : MQTTMessage
{
    public CommMessage() { Kind = MessageKind.SimpleComm; }
    public CommMessage(string message)
    {
        Kind = MessageKind.SimpleComm;
        Message = message;
    }
}

public class RollMessage : MQTTMessage
{
    public RollMessage() { Kind = MessageKind.MoveRoll; }

    public Guid MoveID { get; set; } = new Guid();

    public int RollValue { get; set; }
}



