using PbtASystem.Services.Moves;
using System.Text.Json.Serialization;

namespace PbtASystem.PbtASupport;

public class CharacterSheet
{
	public Guid ID { get; set; } = new Guid();
	public int Blood { get; set; } = 0;
	public int Heart { get; set; } = 0;
	public int Mind { get; set; } = 0;
	public int Spirit { get; set; } = 0;

	public int Mortalis { get; set; } = 0;
	public int Night { get; set; } = 0;
	public int Power { get; set; } = 0;
	public int Veil { get; set; } = 0;

	public int MortalisStatus { get; set; } = 0;
	public int NightStatus { get; set; } = 0;
	public int PowerStatus { get; set; } = 0;
	public int VeilStatus { get; set; } = 0;

	public bool IsMortalisTick { get; set; }
	public bool IsNightTick { get; set; }
	public bool IsPowerTick { get; set; }
	public bool IsVeilTick { get; set; }

	public int Corruption { get; set; }
	public int Armor { get; set; }
	public int Damage { get; set; }

	public AvailableArchetypes Archetype { get; set; }

	public List<USMoveIDs> SelectedArchetypeMoves { get; set; } = new();
	public List<USMoveIDs> SelectedCorruptionMoves { get; set; } = new();
	public List<USMoveIDs> UpgradedMoves { get; set; } = new();

	public string ArchetypeUniqueTittle1 { get; set; } = "";
	public string ArchetypeUniqueTittle2 { get; set; } = "";
	public string ArchetypeUniqueBody1 { get; set; } = "";
	public string ArchetypeUniqueBody2 { get; set; } = "";

	public bool IsBloodScarUsed { get; set; }
	public bool IsHeartScarUsed { get; set; }
	public bool IsSoulScarUsed { get; set; }
	public bool IsMindScarUsed { get; set; }

	public void SetScar(USAttributes attribute, bool isScar)
	{
		switch (attribute)
		{
			case USAttributes.Blood:
				IsBloodScarUsed = isScar;
				if (isScar) Blood--;
				else Blood++;
				break;
			case USAttributes.Heart:
				IsHeartScarUsed = isScar;
				if (isScar) Heart--;
				else Heart++;
				break;
			case USAttributes.Mind:
				IsMindScarUsed = isScar;
				if (isScar) Mind--;
				else Mind++;
				break;
			case USAttributes.Soul:
				IsSoulScarUsed = isScar;
				if (isScar) Spirit--;
				else Spirit++;
				break;
			default:
				break;
		}

		if (isScar) Damage = 0;
	}
	public bool GetScar(USAttributes attribute)
	{
		return attribute switch
		{
			USAttributes.Blood => IsBloodScarUsed,
			USAttributes.Heart => IsHeartScarUsed,
			USAttributes.Soul => IsSoulScarUsed,
			USAttributes.Mind => IsMindScarUsed,
			_ => false
		};
	}

	public int GetAttribute(USAttributes attr)
	{
		return attr switch
		{
			USAttributes.Blood => Blood,
			USAttributes.Mind => Mind,
			USAttributes.Heart => Heart,
			USAttributes.Soul => Spirit,
			USAttributes.Mortality => Mortalis,
			USAttributes.Night => Night,
			USAttributes.Power => Power,
			USAttributes.Veil => Veil,
			_ => 42
		};
	}

	public void SetAttribute(USAttributes attr, int val)
	{
		switch (attr)
		{
			case USAttributes.Blood:
				Blood = val;
				break;
			case USAttributes.Heart:
				Heart = val;
				break;
			case USAttributes.Mind:
				Mind = val;
				break;
			case USAttributes.Soul:
				Spirit = val;
				break;
			case USAttributes.Mortality:
				Mortalis = val;
				break;
			case USAttributes.Night:
				Night = val;
				break;
			case USAttributes.Power:
				Power = val;
				break;
			case USAttributes.Veil:
				Veil = val;
				break;
			case USAttributes.Circle:
				break;
			case USAttributes.None:
				break;
			default:
				break;
		}
	}

	public int GetStatusInCircle(USAttributes attr)
	{
		return attr switch
		{
			USAttributes.Mortality => MortalisStatus,
			USAttributes.Night => NightStatus,
			USAttributes.Power => PowerStatus,
			USAttributes.Veil => VeilStatus,
			_ => 0
		};
	}
	public void SetStatusInCircle(USAttributes attr, int value)
	{
		switch (attr)
		{
			case USAttributes.Mortality:
				MortalisStatus = value;
				break;
			case USAttributes.Night:
				NightStatus = value;
				break;
			case USAttributes.Power:
				PowerStatus = value;
				break;
			case USAttributes.Veil:
				VeilStatus = value;
				break;
			default:
				break;
		}
	}

	public List<bool> AdvancesNormalBools { get; set; } = new List<bool> { false, false, false, false, false, false, false, false};
	public List<bool> AdvancesExtraBools { get; set; } = new List<bool> { false, false, false, false, false, false, false, false };
	public List<bool> AdvancesCorruptionBools { get; set; } = new List<bool> { false, false, false, false, false , false };

public List<Advance> AdvancesNormal { get
		{
			var result = new List<Advance>();
			result.Add(new Advance { Text = "+1 Status (máximo +1)", ID=0, IsUsed = AdvancesNormalBools[0] });
			result.Add(new Advance { Text = "+1 Status (máximo +1)", ID = 1, IsUsed = AdvancesNormalBools[1] });
			result.Add(new Advance { Text = "+1 Status (máximo +1)", ID = 2, IsUsed = AdvancesNormalBools[2] });
			result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 3, IsUsed = AdvancesNormalBools[3] });


			switch (Archetype)
			{
				case AvailableArchetypes.Awaken:
					result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Empieza una nueva relación mortal", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				case AvailableArchetypes.Veteran:
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Unirse o liderar una manada", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				case AvailableArchetypes.Wolf:
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = "Unirse o liderar una manada", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Añade dos características a tu taller", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;

				case AvailableArchetypes.Sworn:
					result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Status en poder: 2", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				case AvailableArchetypes.Mage:
					result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Añade 2 catacterísticas a tu Sanctum", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Aprende 3 hechízos nuevos", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				case AvailableArchetypes.Spectre:
				case AvailableArchetypes.Fair:
				case AvailableArchetypes.Oracle:
				case AvailableArchetypes.Hunter:
				case AvailableArchetypes.Corrupted:
				case AvailableArchetypes.Vampire:
					result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				case AvailableArchetypes.Imp:
					result.Add(new Advance { Text = "Un movimiento de otro arquetipo", ID = 4, IsUsed = AdvancesNormalBools[4] });
					result.Add(new Advance { Text = $"Nuevo movimiento de {Archetype.ToUI()}", ID = 5, IsUsed = AdvancesNormalBools[5] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 6, IsUsed = AdvancesNormalBools[6] });
					result.Add(new Advance { Text = "Cambia tu círculo", ID = 7, IsUsed = AdvancesNormalBools[7] });
					break;
				default:

					break;
			}
			return result;
		}
	}
	public List<Advance> AdvancesExtra
	{
		get
		{
			var result = new List<Advance>();

			switch (Archetype)
			{
				case AvailableArchetypes.Hunter:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Consigue un taller (Veterano)", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Awaken:
					result.Add(new Advance { ID=0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID=1,Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID=2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia tu círculo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Veteran:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira tu personaje a salvo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Vampire:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira tu personaje a salvo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Wolf:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira tu personaje a salvo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Spectre:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cruza al otro lado", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira a salvo a tu personaje", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Sworn:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Borra una cicatriz", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Retira tu personaje a salvo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Mage:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Consigue un aprendiz", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira tu personaje a salvo", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Oracle:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Consigue un Sactum", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira a salvo a tu personaje", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Fair:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Consigue un título noviliario", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Retira a salvo a tu personaje", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Corrupted:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Consigue seguidores demoníacos", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Borra un trabajo de tu contrato", IsUsed = AdvancesExtraBools[7] });
					break;
				case AvailableArchetypes.Imp:
					result.Add(new Advance { ID = 0, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[0] });
					result.Add(new Advance { ID = 1, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[1] });
					result.Add(new Advance { ID = 2, Text = "+1 a cualquier círculo (max +3)", IsUsed = AdvancesExtraBools[2] });
					result.Add(new Advance { ID = 3, Text = "Status 2 en tu própio círculo", IsUsed = AdvancesExtraBools[3] });
					result.Add(new Advance { ID = 4, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[4] });
					result.Add(new Advance { ID = 5, Text = "Avanza 3 movimientos básicos", IsUsed = AdvancesExtraBools[5] });
					result.Add(new Advance { ID = 6, Text = "Consigue Con el demonio dentro (corrupto)", IsUsed = AdvancesExtraBools[6] });
					result.Add(new Advance { ID = 7, Text = "Cambia de arquetipo", IsUsed = AdvancesExtraBools[7] });
					break;
				default:
					break;
			}
			return result;
		}
	}

	public List<Advance> AdvancesCorruptions
	{
		get
		{
			var result = new List<Advance>();
			result.Add(new Advance { ID = 0, Text = "+1 a cualquier atributo (max +3)", IsUsed = AdvancesCorruptionBools[0] });
			result.Add(new Advance { ID = 1, Text = "+1 a cualquier atributo (max +3)", IsUsed = AdvancesCorruptionBools[1] });
			result.Add(new Advance { ID = 2, Text = "nuevo movimiento de corrupción", IsUsed = AdvancesCorruptionBools[2] });
			result.Add(new Advance { ID = 3, Text = "nuevo movimiento de corrupción", IsUsed = AdvancesCorruptionBools[3] });
			result.Add(new Advance { ID = 4, Text = "nuevo movimiento de corrupción de cualquier arquetipo", IsUsed = AdvancesCorruptionBools[4] });
			result.Add(new Advance { ID = 5, Text = "Retira tu personaje, puede volver como un pelígro", IsUsed = AdvancesCorruptionBools[5] });
			return result;
		}
	}
}

public class Advance
{
	public int ID = 0;
	public string Text = "";
	public bool IsUsed;
}
