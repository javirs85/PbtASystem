using MQTTnet.Client;
using PbtASystem.Services.Moves;
using System.Text.Json.Serialization;

namespace PbtASystem.PbtASupport;

public enum Circles { NotSet, Mortalis, Noche, Poder, Velo }
public enum FactionStatuses { NotSet, Creciendo, Manteniendo, Menguando }


public class Rumor
{
	public int ID { get; set; }
	public string Content { get; set; } = "";
	public bool IsSeparator { get; set; } = false;
}

public class Character
{
	public Guid ID { get; set; }
	public bool IsDead { get; set; } = false;
	public bool IsNPC { get; set; } = true;
	public string Name { get; set; } = "Nombre";
	public string Details { get; set; } = "";
	public string Kind { get; set; } = "Noy set";
	public Circles Circle { get; set; } = Circles.NotSet;
	public Guid? FactionID { get; set; } = null;
	public string Aspecto { get; set; } = "aspecto";
	public string Tick { get; set; } = "tick";
	public string FinalGoal { get; set; } = "";
	public string CurrentGoal { get; set; } = "";
	public string MasterSeeds { get; set; } = "";
	

	public Character Duplicate()
	{
		string str = System.Text.Json.JsonSerializer.Serialize(this);
		return System.Text.Json.JsonSerializer.Deserialize<Character>(str);
	}
}

public class Faction
{
	public Guid ID { get; set; }
	public string Name { get; set; } = "Nombre";
	public string Description { get; set; } = "descripción";
	public string Kind { get; set; } = "Tipo";
	public string Assets { get; set; } = "Assets";
	public Circles Circle { get; set; } = Circles.NotSet;
	public string MasterSeeds { get; set; } = "notas secretas";
	public string CurrentlyWorkingOn { get; set; } = "en que están ahora mismo";
	public FactionStatuses Status { get; set; } = FactionStatuses.NotSet;
	public int Size { get; set; }
	public int SizeTemp { get; set; }
	public int Strength { get; set; }
	public int StrengthTemp { get; set; }
	public string SizeExplanation { get; set; } = "por que";
	public string StrengthExplanation { get; set; } = "por que";
	public List<Move> CustomMoves { get; set; } = new();

	public static List<Move> GetGenericFactionMovements = new List<Move>
		{
			new Move
			{
				Name = "Atacar abiertametne a una facción",
				Body = "Cuando una facción ataca abiertamente a otra facción, tira con la diferencia entre los Tamaños de las dos facciones. Con un éxito, la facción atacada sacrifica un activo apropiado o pierde un punto de Tamaño, a su elección. Con un resultado de 7-9, la facción atacante debe sacrificar un asset apropiado o perder también un punto de Tamaño. Con un fallo, el objetivo tiende una trampa inteligente; captura o destruye un Asset o reduce el Tamaño del atacante, a su elección."
			},
			new Move
			{
				Name = "Consolidar el control",
				Body = "Cuando los líderes de una facción consolidan el control sobre sus fuerzas y activos existentes, tiran con su Fuerza. Con un 10+, eligen 2. Con un 7-9, eligen 1:\r\n     · se aseguran nuevas posesiones; marca recursos\r\n     · Buscan nuevos miembros; marcan reclutamiento\r\n     · exigen secreto; encubren otra acción\r\nCon un fallo, sus esfuerzos conducen a luchas internas; una autoridad es destronada o humillada, y una coalición rebelde dentro de la facción gana impulso: reduce la Fuerza de la facción."
			}
			,new Move
			{
				Name = "Localizar a alguien",
				Body = "Cuando una facción intenta localizar a un personaje de estatus 1 o 2 dentro de la ciudad, tira.\r\n   · Si la facción tiene un activo relevante, suma 1.\r\n   · Si su presa es del mismo Círculo, suma 1.\r\n   · Si la facción es de Tamaño-1 o Fuerza-1, resta 1.\r\n   · Si la presa se esconde activamente, resta 1.\r\n\r\nSi la presa es un PNJ: Con un resultado de 7-9, la facción encuentra a su presa; la ataca, la secuestra o se enfrenta a ella con algún coste. Con un 10+, descubren a la presa expuesta o vulnerable; la facción puede actuar impunemente sobre ella. Con un fallo, los intentos de la facción de localizarlos tienen éxito, pero sus agentes lo estropean todo y permiten que la presa escape ilesa.\r\n\r\nSi la presa es un PC: Con un 7-9, la facción rastrea su localización, pero el PC tiene tiempo de prepararse para las fuerzas limitadas que vienen hacia él. Con un 10+, la facción rastreadora saca lo mejor de su presa; acorralan al PC con una fuerza abrumadora o una cuidadosa planificación que le deja poco espacio para evitar a sus perseguidores. Con un fallo, alguien cercano al Pc le avisa antes de tiempo de la búsqueda de la facción... y de una oportunidad o debilidad que el Pc puede explotar..."
			}
			,new Move
			{
				Name = "Incitar al adversario",
				Body = "Cuando una facción intenta incitar a un oponente a cometer un error, tira con la diferencia entre las Fuerzas de las dos facciones. Con un 10+, el objetivo muerde el anzuelo; la facción instigadora asesta un golpe terrible, destruye un activo vulnerable o socava una relación o alianza clave. Con un resultado de 7-9, el objetivo evita lo peor de la trampa, pero causa suficientes problemas como para avergonzarse a sí mismo. Con un fallo, el objetivo se da cuenta del plan; alguien de la facción objetivo acude a uno de los PJ para que le ayude a cambiar las tornas contra la facción instigadora."
			}
			,new Move
			{
				Name = "Apoderarse por la fuerza",
				Body = "Cuando una facción se apodera de algo vulnerable por la fuerza, tira con su Fuerza. Con un éxito, se apoderan de ello; reducen el atributo apropiado de la facción objetivo y se apoderan de un bien vulnerable. Con un 10+, los tres. Con un 7-9, eligen uno:\r\n   · No sacrifican a un líder, aliado o activo importante.\r\n   · no sufren un ataque de represalia inmediato\r\n   · No causan daños colaterales graves.\r\nSi fallan, el ataque resulta en la destrucción total de lo que la facción atacante intentaba apoderarse; alguien acude a uno de los PJ en busca de ayuda para obtener justicia o venganza."
			}
			,new Move
			{
				Name = "Buscar en la ciudad",
				Body = "Cuando una facción busca en la ciudad información útil -un raro conocimiento oculto, las debilidades de otra facción, la localización de un artefacto- tira con su Tamaño. Con un éxito, descubren algunos detalles cruciales, suficientes para pedir a un PJ o PNJ notable que siga investigando. Con un 7-9, también eligen 2. Con un 10+, eligen 1.\r\n   - atraen la atención de una facción rival\r\n   - tienen que hacer vulnerable un activo\r\n   - tienen -1 en curso hasta el final del siguiente turno de\r\n     la facción. Con un fallo, un miembro de la facción que \r\n     ha conseguido alguna información vital acaba muerto \r\n     o desaparecido... pero no antes de transmitir lo que \r\n     sabe a uno de los PJ."
			}
			,new Move
			{
				Name = "Ofrecer pasaje",
				Body = "Cuando una facción ofrece paso a alguien -para entrar o salir de la ciudad- tira con su Tamaño. Con un acierto, el camino queda expedito, sin importar quién se oponga; elige 1:\r\n   · alguien sale; queda fuera de su alcance hasta \r\n     regrese\r\n   · alguien entra; la facción gana una poderosa baza\r\n\r\nCon un 7-9, el paso ofende a un PNJ de Estatus 3 que busca tributo por la intrusión; la facción debe realizar un favor -dedicar un movimiento de facción en el próximo turno de facción- sacrificar un activo o arriesgarse a una guerra abierta. Con un fallo, el paso provoca un conflicto entre la facción y sus propios aliados antes de que pueda completarse; alguien acude a uno de los PJ en busca de ayuda para negociar una tregua."
			}
		};
	[JsonIgnore]
	public List<Move> GenericFactionMovements
	{
		get { return Faction.GetGenericFactionMovements; }
	}
}
public class Move
{
	public string Name { get; set; } = "";
	public string Body { get; set; } = "";
}

public class QandA
{
	public string Question { get; set; } = "";
	public string Answer { get; set; } = "";
}

public class FactionAction
{
	public Guid Faction;
	public string Description { get; set; } = "";
}

public class Debt
{
	public Debt() { ID = Guid.NewGuid(); }
	public int Amount { get; set; } = 1;
	public string Reason { get; set; } = "";
	public Guid ID { get; set; }
	public Guid PayingID { get; set; }
	public Guid ReceivingID { get; set; }
}

public class Chronicle
{
	public Guid ID { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = "";
	public List<Guid> CharacterIDs { get; set; } = new();
	public List<Guid> FactionIDs { get; set; } = new();
	public string MasterPlayerID { get; set; } = "";
	public List<MapPlayerCharacter> PlayerLinks { get; set; } = new();
	public List<Clock> Clocks { get; set; } = new();
}


public class Clock
{
	public Guid ID { get; set; } = new Guid();
	public string Title { get; set; } = "";
	public int Value { get; set; } = 0;
	public List<string> events { get; set; } = new();
	public string Event1 { get { return events[0]; }set { events[0] = value; } }
	public string Event2 { get { return events[1]; }set { events[1] = value; } }
	public string Event3 { get { return events[2]; }set { events[2] = value; } }
	public string Event4 { get { return events[3]; }set { events[3] = value; } }
	public string Event5 { get { return events[4]; }set { events[4] = value; } }
	public string Event6 { get { return events[5]; }set { events[5] = value; } }
	public string Event7 { get { return events[6]; }set { events[6] = value; } }
	public string Event8 { get { return events[7]; }set { events[7] = value; } }
	public string Event9 { get { return events[8]; }set { events[8] = value; } }

	public void Minus()
	{
		Value = Math.Max(0, Value-1);
	}
	public void Plus() =>Value = Math.Min(8, Value+1);
	

	public Clock()
	{

		ID = Guid.NewGuid();
		Title = "Nuevo reloj";
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
		events.Add("");
	}
}

public class MapPlayerCharacter
{
	public string PlayerID { get; set; } = "";
	public Guid CharacterID { get; set; } = new();
	public Guid? CharacterSheetID { get; set;} = new();
}