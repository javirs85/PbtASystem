using PbtASystem.Services.Moves;
using System.Text.Json.Serialization;

namespace PbtASystem.PbtASupport
{
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
		public List<USMoveIDs> UpgradedMoves { get; set; }  = new();

		public string ArchetypeUniqueTittle1 { get; set; } = "";
		public string ArchetypeUniqueTittle2 { get; set; } = "";
		public string ArchetypeUniqueBody1 { get; set; } = "";
		public string ArchetypeUniqueBody2 { get; set; } = "";

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
	}
}
