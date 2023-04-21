using PbtASystem.PbtASupport;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;

namespace PbtASystem.Services.Moves;

public class USMovesService : MovementsProviderBase
{
	public List<USMove> AllMovements { get; set; } = new();
	public USMovesService() {
		AllMovements.AddRange(GenerateBasicMovments());

		AllMovements.Add(new USMove(USMoveIDs.B_LiberarPoder, USAttributes.Soul) { Arquetipe = AvailableArchetypes.All, Tittle= "Liberar tu poder" });

		AllMovements.AddRange(GenerateFactionMovements());
		AllMovements.AddRange(GenerateDebtMovements());

		AllMovements.AddRange(GenerateHunterMovements());
		AllMovements.AddRange(GenerateAwakenMovements());
		AllMovements.AddRange(GenerateVeteranMovements());
		AllMovements.AddRange(GenerateWolfMovements());
		AllMovements.AddRange(GenerateVampireMovementes());
		AllMovements.AddRange(GenerateMageMovements());
		AllMovements.AddRange(GenerateOracleMovements());
		AllMovements.AddRange(GenerateCorruptedMovements());
		AllMovements.AddRange(GenerateFairyMovements());
		AllMovements.AddRange(GenerateSpectreMovements());
		AllMovements.AddRange(GenerateSwornMovements());
		AllMovements.AddRange(GenerateImpMovements());

		AllMovements.AddRange(GenerateHunterCorruptionMovements());
		AllMovements.AddRange(GenerateAwakenCorruptionMovements());
		AllMovements.AddRange(GenerateVeteranCorruptionMovements());
		AllMovements.AddRange(GenerateWolfCorruptionMovements());
		AllMovements.AddRange(GenerateVampireCorruptionMovements());
		AllMovements.AddRange(GenerateMageCorruptionMovements());
		AllMovements.AddRange(GenerateOracleCorruptionMovements());
		AllMovements.AddRange(GenerateCorrupteCorruptionMovements());
		AllMovements.AddRange(GeneratefairyCorruptionMovements());
		AllMovements.AddRange(GenerateSpectreCorruptionMovements());
		AllMovements.AddRange(GenerateSwornCorruptionMovements());
		AllMovements.AddRange(GenerateImpCorruptionMovements());

		AllMovements.AddRange(GenerateHunterDramaticMovements());
		AllMovements.AddRange(GenerateAwakenDramaticMovements());
		AllMovements.AddRange(GenerateVeteranDramaticMovements());
		AllMovements.AddRange(GenerateWolfDramaticMovements());
		AllMovements.AddRange(GenerateVampireDramaticMovements());
		AllMovements.AddRange(GenerateMageDramaticMovements());
		AllMovements.AddRange(GenerateOracleDramaticMovements());
		AllMovements.AddRange(GenerateCorruptedDramaticMovements());
		AllMovements.AddRange(GenerateFairyDramaticMovements());
		AllMovements.AddRange(GenerateSpectreDramaticMovements());
		AllMovements.AddRange(GenerateSwornDramaticMovements());
		AllMovements.AddRange(GenerateImpDramaticMovements());

		AllMovements.Add(new USMove(USMoveIDs.rawBlood, USAttributes.Blood) { Tittle = "Sangre" });
		AllMovements.Add(new USMove(USMoveIDs.rawHeart, USAttributes.Heart) { Tittle = "Corazón" });
		AllMovements.Add(new USMove(USMoveIDs.rawSoul, USAttributes.Soul) { Tittle = "Espíritu" });
		AllMovements.Add(new USMove(USMoveIDs.rawMortal, USAttributes.Mortality) { Tittle = "Mortalis" });
		AllMovements.Add(new USMove(USMoveIDs.rawNight, USAttributes.Night) { Tittle = "Noche" });
		AllMovements.Add(new USMove(USMoveIDs.rawPower, USAttributes.Power) { Tittle = "Poder" });
		AllMovements.Add(new USMove(USMoveIDs.raw2d6, USAttributes.None) { Tittle = "2d6" });
	}

	public List<USMoveIDs> GetInitialMovesIDsByArchetype(AvailableArchetypes arch)
	{
		var list = from mov in AllMovements
			   where mov.Arquetipe == arch && mov.TypeOfMovement == USMove.MovementTypes.ArquetipeMovement && mov.IsSelected
			   select mov.ID;

		return list.ToList();
	}

#region overriden methods
	public override IMovement GetMovement<TMovIDs, TClases>(TMovIDs _ID, TClases _archetype)
	{
		USMoveIDs ID = (USMoveIDs)(object)_ID;
		AvailableArchetypes archetype = (AvailableArchetypes)(object)_archetype;

		if (ID == USMoveIDs.B_LiberarPoder)
		{
			var m = GetArchetipeBasedLetItOutMovement(archetype);
			var original = AllMovements.Find(x => x.ID == USMoveIDs.B_LiberarPoder);
			original.PreCondition = m.PreCondition;
			original.AvancedConsequences = m.AvancedConsequences;

			return original;
		}

		return AllMovements.Find(x => x.ID == ID) ?? new USMove(USMoveIDs.NotSet, USAttributes.None) { Tittle = "Unknown movement" };
	}

	public override IMovement GetMovement<TMovIDs>(TMovIDs _ID)
	{
		USMoveIDs ID = (USMoveIDs)(object)_ID;
		return AllMovements.Find(x => x.ID == ID) ?? new USMove(USMoveIDs.NotSet, USAttributes.None) { Tittle = "Unknown movement" };
	}

	USMove GetArchetipeBasedLetItOutMovement(AvailableArchetypes archetype)
	{
		var m = new USMove(USMoveIDs.B_LiberarPoder, USAttributes.Soul)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Liberar tu poder",
			PreCondition = new Consequences
			{
				MainText = "Tira con Espíritu. Con 7+ marcate corrupción y el Master tedirá cómo el efecto de tu poder es costoso, limitado o inestable. Con un 10+ elije ignorar la corrupción o las complicaciones.",
			},
			AvancedConsequences = new Consequences
			{
				MainText = " 12+, tus poderes o habilidades se manifiestan de alguna forma inesperada pero útil. Puedes márcate corrupción para hacer permanente esa manifestación.",
			}
		};

		m.Tittle = "Liberar tu poder";

		switch (archetype)
		{
			case AvailableArchetypes.Hunter:
				m.PreCondition.Options = new List<string> {
						"Encuentra y persigue a algien o algo por la ciudad con rastros o información limitados.",
						"Improvisa un arma (daño2, mano, desastrosa) o (armadura-1, frágil).",
						"Improvisa un explosivo (daño3, ruidoso, fuego) o bomba de humo (aturdidor, ruidosa, humo).",
						"Forzar a un enemigo vulnerable a huir de tu\r\npresencia y entregar un mensaje.",
					};
				break;
			case AvailableArchetypes.Awaken:
				m.PreCondition.Options = new List<string> {
						"Consigue acceso a una zona asegurada o bloqueada",
						"Atrae atencion inmediata de los mortales sobre una persona o situación",
						"Encuentra una pista o ventaja en un area inmediata que antes se había pasado por alto",
						"Convence a un PNJ de que actue con bondad, interés o en propio beneficio",
					};
				break;
			case AvailableArchetypes.Veteran:
				m.PreCondition.Options = new List<string> {
						"Sorprende a un enemigo desprevenido con un golpe terrible o dejalo K.O.",
						"Proteje un lugar, o genera barricadas con materiales mínimos",
						"Asusta o intimida a alguien con un recordatorio de quien solias ser",
						"Revela la forma en que un antiguo aliado o enemigo está dando forma a un conflicto actual",
					};
				break;
			case AvailableArchetypes.Vampire:
				m.PreCondition.Options = new List<string> {
						"Crea una oportunidad para ESCAPAR, ignorando impedimentos mortales",
						"Realiza una azaña fantástica de fuerza o agiliad vampírica",
						"Extiende tus sentidos vampíricos por un corto periodo de tiempo",
						"Muestra dominancia: PNJ de bajo nivel se van, PJ deben MANTENER LA CALMA",
					};
				break;
			case AvailableArchetypes.Wolf:
				m.PreCondition.Options = new List<string> {
						"Curate 2-daño inmediatamente empezando por daño crítico",
						"Transformate sin necesidad de la Luna",
						"Realiza una proheza de fuerza o velocidad lupína",
						"Aumenta tus sentidos lupinos a niveles supernaturales",
					};
				break;
			case AvailableArchetypes.Spectre:
				m.PreCondition.Options = new List<string> {
						"Transportate instantaneamente a uno de tus anclas, sin importar la distancia",
						"Toma control de una maqina o vehículo poseyendo su forma mecánica",
						"Desata un rayo de energía extoplasmática (daño2, cerca, area, perforante)",
						"Sige a un mortal ordinario, sin importar a donde vaya",
					};
				break;
			case AvailableArchetypes.Mage:
				m.PreCondition.Options = new List<string> {
						"Deflecta o redirige un golpe justo antes de que te toque",
						"Realiza una proheza de fuerza o precisión telekinésica",
						"Detecta la presencia o funcionamiento de objetos mágicos o hechizos",
						"Cambia la forma o naturaleza de un objeto expuesto o hechizo mágico",
					};
				break;
			case AvailableArchetypes.Oracle:
				m.PreCondition.Options = new List<string> {
						"Expón la verdad esencial de una cosa o persona en tu presencia",
						"Manipula las hebras de la realidad para ayudar o perjudicar un PNJ en tu presencia",
						"Impresiona o asusta a alguien con un conocimiento del pasado",
						"Canaliza una potente profecía del más allá sobre un personaje presente",
					};
				break;
			case AvailableArchetypes.Fair:
				m.PreCondition.Options = new List<string> {
						"Invoca una tormenta elemental de tu corte (daño2, cerca, area, penetrante)",
						"Aparece ante los demás como alguein a quien anteriormente has tocado",
						"Obliga a los elementos de tu corte a explicarte lo que han visto",
						"Crea un vinculo telepático con alguien durante una escena",
					};
				break;
			case AvailableArchetypes.Corrupted:
				m.PreCondition.Options = new List<string> {
						"Impregna tu tacto de corrupción demoníaca (daño2, intimidante, penetrante)",
						"Impresiona, asusta o consterna a alguien con una demostración de furia demoníaca",
						"Atraviesa un obstaculo físico creado por manos mortales",
						"Invoca a tu patrón oscuro directamente donde estas",
					};
				break;
			default:
				break;
		}

		return m;
	}

#endregion

	private List<USMove> GenerateBasicMovments()
	{
		List<USMove> result = new();

		result.Add(new USMove(USMoveIDs.B_Ataque, USAttributes.Blood)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			Tittle = "Lanzar un ataque",
			IsImproved = false,
			PreCondition = new Consequences
			{
				MainText = "Cuando le lances un ataque a alguien,  tira con Sangre. Si superas la tirada, inflige daño según lo establecido y TU OPONENTE elige 1 opción:",
				Options = new List<string>
					{
						"Te hiere a tí también.",
						"Te pone en una situación complicada.",
						"Crea una oportunidad para escapar"
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 10+, TU eliges también 1 de las siguientes opciones:",
				Options = new List<string>
					{
						"Haces un daño terrible",
						"Obtienes algo de él.",
						"Creas una oportunidad para un aliado"
					}
			},
			AvancedConsequences = new Consequences
			{
				MainText = "Con un 12+, infliges daño según lo establecido y eliges 2 de la lista de 10+",
			}

		});
		result.Add(new USMove(USMoveIDs.B_Escapar, USAttributes.Blood)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Escapar de una situación",
			PreCondition = new Consequences
			{
				MainText = "Cuando *aproveches una oportunidad** para escapar de una situación, tira con Sangre."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si tienes éxito, escapas y eliges 1. Con un 7-9, el MC también elige uno (2 en total):",
				Options = new List<string>
					{
						"Sufres daño durante tu huida",
						"acabas en otra situación peligrosa",
						"te dejas algo importante",
						"tienes una deuda con un PNJ por su ayuda",
						"cedes a tu naturaleza básica y marcas la corrupción"
					}
			},
			AvancedConsequences = new Consequences
			{
				MainText = "12+  escapas y haces un descubrimiento importante.",
			}
		});
		result.Add(new USMove(USMoveIDs.B_Convencer, USAttributes.Heart)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Persuadir a un PNJ",
			PreCondition = new Consequences
			{
				MainText = "Cuando persuades  a un *PNJ** mediante seducción, promesas o amenazas,  tira con Corazón. Si superas la tirada, comparten tu opinión y hacen lo que les pides.",
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, se oponen a tu propuesta o exigen un pago -una deuda, un favor, recursos- antes de aceptar seguir adelante. Si cobras una Deuda con el PNJ *antes** de tirar el dado, suma +3 a tu total"
			},
			AvancedConsequences = new Consequences
			{
				MainText = "12+, hará lo que le hayas pedido y te ayudará con ese asunto hasta el final.",
			}
		});
		result.Add(new USMove(USMoveIDs.B_Calar, USAttributes.Mind)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Calar a alguien",
			PreCondition = new Consequences
			{
				MainText = "Cuando intentes calar a alguien, tira con Mente. Si superas la tirada, obtienes 2 puntos. Mientras estés interactuando con ese personaje, gasta cada punto en hacerle una pregunta al jugador que lo interpreta. Si pertenecéis a la mismo Circulo, hazle una pregunta más, aunque hayas fallado la tirada."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, la otra persona obtiene también 1 punto que puede gastar en preguntarte a ti. ",
				Options = new List<string>
					{
						"¿Quién mueve los hilos de tu personaje?",
						"¿Qué problema tiene tu personaje con (...)?",
						"¿Qué espera conseguir tu personaje de (...)?",
						"¿Cómo podría yo hacer que tu personaje (...)?",
						"¿Qué le preocupa a tu personaje que ocurra?",
						"¿Cómo podría yo hacer que tu personaje contrajera una Deuda conmigo?"
					}
			},
			AvancedConsequences = new Consequences
			{
				MainText = "12+, puedes hacer cualquier pregunta (una por cada punto), no solo las de la lista. Pregunta lo que quieras, incluso «¿Estás mintiendo?». Tu objetivo es un libro abierto.",
			}
		});
		result.Add(new USMove(USMoveIDs.B_Confundir, USAttributes.Mind)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Confundir, distraer o engañar",
			PreCondition = new Consequences
			{
				MainText = "Cuando intentes confundir, distraer o engañar a alguien, tira con Mente. Si superas la tirada, se lo traga, al menos durante un momento.",
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, elige dos opciones:"
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, elige 3 opciónes.",
				Options = new List<string>
					{
						"Creas una oportunidad.",
						"Revelas un punto débil o un defecto.",
						"Dejas a tu objetivo confundido durante un tiempo.",
						"Evitas más complicaciones."
					}
			},
			AvancedConsequences = new Consequences
			{
				MainText = "12+, obtienes las 4 opciones. Además, duplica el efecto de una de ellas.",
			}
		});
		result.Add(new USMove(USMoveIDs.B_KeepCalm, USAttributes.Soul)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			Tittle = "Mantener la calma",
			PreCondition = new Consequences
			{
				MainText = "Cuando las cosas se pongan serias y mantengas la calma, dile al Maestro de Ceremonias qué situación quieres evitar y tira con Espíritu.",
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, el Maestro de Ceremonias te dirá el coste que conlleva para ti."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, todo va bien."
			},
			AvancedConsequences = new Consequences
			{
				MainText = "12+, la calma de tu oposición se ve comprometida. Dile lo que le va a costar mantener su curso de acción actual.",
			}
		});
		result.Add(new USMove(USMoveIDs.B_AyudarOFastidiar, USAttributes.Circle)
		{
			TypeOfMovement = USMove.MovementTypes.BasicMovements,
			IsSelected = true,
			IsImproved = false,
			TicksCircle = true,
			Tittle = "Ayudar o molestar",
			PreCondition = new Consequences
			{
				MainText = "Cuando ayudes o te interpongas en el camino de un PJ. *Después de que haya tirado**, tira con su Círculo. Si aciertas, dale un +1 o -2 a su tirada",
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un resultado de 7-9, te expones a peligro, enredo o coste"
			}
		});

		return result;
	}
	private List<USMove> GenerateFactionMovements()
	{
		List<USMove> result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.F_01, USAttributes.Circle)
		{
			TypeOfMovement = USMove.MovementTypes.FactionMovement,
			IsSelected = true,
			Tittle = "Echarse a la calle",
			TicksCircle = true,
			PreCondition = new Consequences
			{
				MainText = "Cuando *te eches a la calle para conseguir lo que quieres**,  di el nombre de la persona a la que acudes y tira con su Circulo. Si superas la tirada, estará disponible y tendrá lo que quieres."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, elige 1 opción:",
				Options = new List<string>
					{
						"La persona a la que acudes ya tiene problemas propios con los que lidiar.",
						"Lo que necesitas tiene un coste mayor de lo que esperabas."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.F_02, USAttributes.Circle)
		{
			TypeOfMovement = USMove.MovementTypes.FactionMovement,
			IsSelected = true,
			TicksCircle = true,
			Tittle = "Ponerle cara a un nombre",
			PreCondition = new Consequences
			{
				MainText = "Cuando le pongas cara a un nombre o un nombre a una cara, tira con su Circulo. Si fallas la tirada no lo conoces o tienes una Deuda con él; el MC te dirá cuál de las dos."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si superas la tirada, conoces su reputación; el Maestro de Ceremonias te dirá lo que la mayoría de la gente sabe sobre él. "
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, ya has tratado antes con esa persona; descubres algo interesante y útil sobre él o tiene una Deuda contigo. (tu elijes)"
			}
		});
		result.Add(new USMove(USMoveIDs.F_03, USAttributes.Circle)
		{
			TypeOfMovement = USMove.MovementTypes.FactionMovement,
			IsSelected = true,
			TicksCircle = true,
			Tittle = "Investigar un lugar de poder",
			PreCondition = new Consequences
			{
				MainText = "Cuando investigues un lugar poderoso, tira con el Circulo a la que pertenezca."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si superas la tirada, verás la realidad subyacente bajo la superficie."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, puedes hacerle una pregunta al Maestro de Ceremonias sobre las tramas y la política de esa Facción."
			}
		});
		return result;
	}
	private List<USMove> GenerateDebtMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.DebtMovements,
			IsSelected = true,
			Tittle = "Hacerle un favor a alguien",
			PreCondition = new Consequences
			{
				MainText = "Cuando le hagas un favor a alguien, *sin recibir nada a cambio** contrae una Deuda contigo."
			}
		});
		result.Add(new USMove(USMoveIDs.D_05, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.DebtMovements,
			IsSelected = true,
			Tittle = "Saldar una deuda",
			TicksCircle = true,
			PreCondition = new Consequences
			{
				MainText = "Cuando pagues una deuda, haz lo reclamado y marca el círculo del acreedor de la deuda."
			}
		});
		result.Add(new USMove(USMoveIDs.D_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.DebtMovements,
			IsSelected = true,
			Tittle = "Meterte en sus asuntos",
			PreCondition = new Consequences
			{
				MainText = "Cuando *interfieres en los negocios de alguien**, le debes una Deuda. \r\n"
			}
		});
		result.Add(new USMove(USMoveIDs.D_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.DebtMovements,
			IsSelected = true,
			TicksCircle = true,
			Tittle = "Cobrarse una deuda",
			PreCondition = new Consequences
			{
				MainText = "Cuando te cobres una Deuda, recuérdale a tu deudor por qué está en Deuda contigo."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Para hacer que un personaje jugador:",
				Options = new List<string>
					{
						"Te haga un favor de coste moderado.",
						"Te eche una mano.",
						"Fastidie a otro.",
						"Responda a una pregunta con sinceridad.",
						"Cancele una Deuda de la que sea acreedor.",
						"Te transfiera a ti el derecho a cobrarle una Deuda a otra persona.",
					}
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Para hacer que un personaje NO jugador:",
				Options = new List<string>
					{
						"Responda con sinceridad a una pregunta sobre su Facción.",
						"Organice un encuentro con un PNJ de su Círculo.",
						"Te haga un regalo valioso y útil.",
						"Cancele una Deuda de la que sea acreedor.",
						"Te transfiera a ti el derecho a cobrarle una Deuda a otra persona.",
						"Te dé un +3 para Convencerlo (esta opción ha de elegirse antes de tirar).",
					}
			}
		});
		result.Add(new USMove(USMoveIDs.D_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.DebtMovements,
			IsSelected = true,
			Tittle = "Negarse a pagar una deuda",
			PreCondition = new Consequences
			{
				MainText = "Cuando te niegues a pagar una Deuda, tira con la diferencia de estatus entre tú y tu acreedor., eludes el trato actual, pero sigues en Deuda. Con un éxito, te libras de la obligación por ahora, pero sigues debiendo la Deuda. Si fallas, no puedes evitar la soga: o honras la Deuda en su totalidad o borras todas las Deudas que te debe su Círculo y tener un -1 en curso al Estatus con su Círculo hasta que pase el tiempo."
			}
		});
		return result;
	}


	private List<USMove> GenerateHunterMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Hunt_01, USAttributes.Blood)
		{
			Tittle = "Exterminador",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando mantengas la calma durante una cacería, tira con Sangre en vez de Espíritu."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_02, USAttributes.None)
		{
			Tittle = "Letal",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando hagas daño suma +1"
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_03, USAttributes.Mind)
		{
			Tittle = "Leyendo también se aprende",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando te encuentres un tipo nuevo de criatura sobrenatural, tira con Mente. Si fallas la tirada, malinterpretas a la criatura; el Maestro de Ceremonias te dirá cómo."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si superas la tirada, el Maestro de Ceremonias te dirá un poco sobre ella y cómo se la puede matar."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10 +, tras oír la información, hazle una pregunta al Maestro de Ceremonias; la contestará con honestidad. "
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_04, USAttributes.Mind)
		{
			Tittle = "Piso franco",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Tienes un lugar seguro en el que esconderte. Detállalo y elige 3 rasgos que tenga:",
				Options = new List<string>
					{
						"Dispositivos de vigilancia de alta tecnología.",
						"Una prisión mística.",
						"Muros / ventanas / puertas reforzados.",
						"Agua y comida para una semana.",
						"Explosivos dispuestos para volar el lugar."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_05, USAttributes.Blood)
		{
			Tittle = "¡Por aquí!",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando guíes a alguien para ponerlo a salvo, tira con Sangre. Si fallas la tirada, todos quedan a salvo excepto tú; te dejan atrás y la salida se cierra para ti."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7 - 9, o bien tú sufres daño o bien uno de ellos sufre daño(a tu elección)."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10 +, todos conseguís escapar sanos y salvos."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_06, USAttributes.Blood)
		{
			Tittle = "¿Quieres tentar a la suerte?",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando convenzas a un personaje no jugador mientras blandes un arma de 2-daño o superior, tira con Sangre en vez de Corazón."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Hunt_07, USAttributes.None)
		{
			Tittle = "Preparado para todo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = " Tienes una armería bien provista, llena de armas tanto antiguas como modernas. Cógete otra arma personalizada o añádele otra característica a cada una de las que ya tengas."
			}
		});

		return result;
	}
	private List<USMove> GenerateAwakenMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Awak_01, USAttributes.Mind)
		{
			Tittle = "Fisgón",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = " Cuando estés ojo avizor por si hay problemas, tira con Mente. " +
				 "Mientras estés ahí, gasta los puntos obtenidos para hacerle preguntas al Maestro de Ceremonias, una por cada punto."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, obtienes 1 punto."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Cuando estés ojo avizor por si hay problemas, tira con Mente.",
				Options = new List<string>
					{
						"¿Cuál es la mejor vía de entrada o salida de que dispongo?",
						"¿Quién o qué hay aquí que no sea lo que parece?",
						"¿Qué ha pasado aquí recientemente?",
						"¿Cuál es el mayor peligro que hay aquí para mí?",
						"¿De quién es este territorio?"
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Awak_02, USAttributes.Mind)
		{
			Tittle = "Los deberes hechos",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando le pongas cara al nombre de alguien con importancia política (a tu juicio), tira con Mente en vez de Facción. Si fallas, tus fisgoneos ya te han metido en problemas con esa persona y sabe que has estado metiéndote en sus asuntos."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si superas la tirada, sabes un secreto peligroso sobre él o sus maquinaciones políticas."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, sabes cómo usar esta información como influencia; además, tiene una Deuda contigo, apúntatela.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Awak_03, USAttributes.Mind)
		{
			Tittle = "Conozco a un tío",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando te eches a la calle o le pongas cara a un nombre de la Mortalidad, tira con Mente en vez de Facción."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Awak_04, USAttributes.None)
		{
			Tittle = "Vengo con amigos",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando te cobres una Deuda de un personaje no jugador, añade esta opción a la lista:",
				Options = new List<string>
					{
						"Te apoye en una situación peligrosa."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Awak_05, USAttributes.Mind)
		{
			Tittle = "Tirador avezado",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando lances un ataque con un arma de fuego,  tira con Mente en vez de Sangre."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Awak_06, USAttributes.None)
		{
			Tittle = "Duro de pelar",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando te metas en problemas mientras sigues una pista,  obtienes armadura+1."
			}
		});


		return result;
	}
	private List<USMove> GenerateVeteranMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Vet_01, USAttributes.Mind)
		{
			Tittle = "Viejos amigos, viejos favores",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando te encuentres por primera vez a un personaje no jugador, en vez de ponerle nombre a una cara, puedes declarar que es un viejo amigo y tirar con Mente. Si superas la tirada, te ofrecerá consuelo y ayuda, aunque por ello se exponga a sufrir peligro o represalias."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, dile al Maestro de Ceremonias por qué tienes una Deuda con esa persona."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Si fallas la tirada, dile al Maestro de Ceremonias por qué te quiere muerto."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vet_02, USAttributes.None)
		{
			Tittle = "Auténtico artista",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando crees algo para alguien con tu taller, márcate su Facción."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vet_03, USAttributes.Mind)
		{
			Tittle = "Invertir",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando alguien tenga 2 o más Deudas contigo y le eches una mano o le fastidies, tira con Mente en vez de Facción."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vet_04, USAttributes.None)
		{
			Tittle = "¡Ya estoy viejo para estas mierdas!",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando te veas en medio de una pelea que hayas tratado de impedir, obtienes armadura+1 y +1 a todas las tiradas destinadas a poneros a salvo a los demás y a ti."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vet_05, USAttributes.Mind)
		{
			Tittle = "El plan perfecto",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando traces un plan con alguien, tira con Mente." +
							"Mientras el plan se lleva a cabo, puedes gastar los puntos, uno por opción.",
				Options = new List<string>
					{
						"Sumarle 1 a la tirada de alguien (elige esta opción después de esa tirada)",
						"Descartar todo el daño que alguien sufra de un único ataque.",
						"Garantizar que los tuyos tengan a mano exactamente el equipo que necesitan."
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, obtienes 2 puntos."
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = " Con un +10, obtienes 3 puntos.",
			},
			ConsequencesOn6 = new Consequences
			{
				MainText = "Si fallas la tirada, obtienes 1 punto, pero tu plan se desmorona en el peor momento."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vet_06, USAttributes.Mind)
		{
			Tittle = "Sacar la pistola en una pelea de navajas",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando lances un ataque con el que agraves seriamente el conflicto, tira con Mente en vez de Sangre."
			}
		});

		return result;
	}
	private List<USMove> GenerateWolfMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Wolf_01, USAttributes.Blood)
		{
			Tittle = "Reconocer el terreno",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Si al principio de la sesión estás patrullando activamente tu territorio, tira con Sangre. Si fallas o si no prestas atención a tu territorio, la situación se va a pique y tus problemas van cuesta abajo y sin frenos."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7 - 9, uno de tus problemas(a tu elección) aflora, pero la situación está estable en su mayoría. "
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10 +, tu territorio está seguro y los problemas son mínimos; obtienes + 1 a todas las tiradas para echarte a la calle en tu territorio."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Wolf_02, USAttributes.Blood)
		{
			Tittle = "Sabueso",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando vayas a la caza de alguien,  tira con Sangre. Si fallas, algo desagradable te encuentra a ti primero."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si superas la tirada, sabes exactamente dónde encontrarlo y puedes seguir su olor hasta dar con él. "
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, obtienes +1 a la siguiente tirada contra él."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Wolf_03, USAttributes.None)
		{
			Tittle = "Regeneración",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando liberes tu poder, añade esta opción a la lista:",
				Options = new List<string>
					{
						"Se te cierran las heridas; cúrate 1-daño."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Wolf_04, USAttributes.Heart)
		{
			Tittle = "Lobo alfa",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando convenzas a un personaje no jugador en tu territorio, tira con Sangre en vez de Corazón."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Wolf_05, USAttributes.Soul)
		{
			Tittle = "Desde el borde del abismo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Puedes abandonar tu forma lupina a voluntad. Cuando lo hagas, tira con Espíritu. Si fallas la tirada, te transformas, pero la transformación es incompleta, lenta o dolorosa. Si superas la tirada, vuelves a tu forma original."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, sufres 1-daño o te marcas corrupción."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Wolf_06, USAttributes.None)
		{
			Tittle = "Temerario",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = " Si te tiras de cabeza al peligro sin cubrirte las espaldas,  obtienes armadura+1. Si estás liderando a un grupo, este también obtiene armadura+1."
			}
		});

		return result;
	}
	private List<USMove> GenerateVampireMovementes()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Vamp_01, USAttributes.Blood)
		{
			Tittle = "Hambre eterna",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Ansías sangre, emociones o carne humanas, elige una. Cuando te alimentes, tira con Sangre. Si fallas, algo sale terriblemente mal."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con un 7-9, elige 2"
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Con un 10+, elige 3",
				Options = new List<string>
					{
						"Te curas 1-daño.",
						"Descubres un secreto sobre esa persona.",
						"Obtienes +1 a la siguiente tirada.",
						"Tu víctima no muere."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vamp_02, USAttributes.None)
		{
			Tittle = "Irresistible",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando convenzas a un personaje no jugador mediante promesas o seducción, trata los resultados de 7-9 como si fueran de 10+. Si fallas la tirada, tus maquinaciones tienen éxito como si hubieras sacado un 7-9, pero atraes la atención de un enemigo o rival"
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vamp_03, USAttributes.None)
		{
			Tittle = "Refugio",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Tienes un lugar seguro, a salvo de peligros externos. Tiene raciones de emergencia, unos cuantos gules y una vía de escape. Cuando alguien venga a tu refugio por voluntad propia, entra en tu red."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vamp_04, USAttributes.Soul)
		{
			Tittle = "Sangre fría",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando mantengas la calma bajo presión emocional, tira con Sangre en vez de Espíritu."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vamp_05, USAttributes.Blood)
		{
			Tittle = "Mantener cerca a tus amigos",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando cales a alguien satisfaciendo sus vicios, tira con Sangre en vez de Mente."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Vamp_06, USAttributes.None)
		{
			Tittle = "Que corra la voz",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando le cobres una Deuda a alguien de tu red, añade esta opción a la lista:",
				Options = new List<string>
					{
						"Haz correr la voz entre su Facción de que quieres algo.",
						"Obtienes +3 a la siguiente tirada de echarte a la calle con esa Facción."
					}
			}
		});

		return result;
	}
	private List<USMove> GenerateMageMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Mage_01, USAttributes.Soul)
		{
			Tittle = "Canalizar",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando canalices y reúnas tu magia, tira con Espíritu. Con un 10+, obtienes 3 puntos. Con un 7-9, obtienes 3 puntos y eliges 1 opción de la lista que aparece a continuación. Si fallas, obtienes 1 punto, pero no puedes volver a canalizar en esta escena.",
				Options = new List<string>
					{
						"Obtienes –1 a todas las tiradas hasta que descanses.",
						"Sufres 1-daño (perforante)",
						"Te marcas corrupción."
					}
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Los puntos te durarán hasta que los gastes. Puedes gastarlos para lanzar cualquier hechizo que tengas según se indique en su descripción."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_02, USAttributes.Soul)
		{
			Tittle = "Sanctasanctórum",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando vayas a tu santuario a por un ingrediente para un hechizo, una reliquia o un libro, tira con Espíritu. Con un 10+, tienes algo que vale para lo que necesitas. Con un 7-9 , tienes algo que se le acerca, pero le falta o le falla algo importante. Si fallas, no tienes lo que estás buscando, pero conoces a alguien que probablemente sí lo tenga.",
			}
		});

		result.Add(new USMove(USMoveIDs.A_Mage_04, USAttributes.None)
		{
			Tittle = "HECHIZO: Rastrear",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Gasta 1 punto para descubrir dónde se encuentra alguien.Tienes que tener un objeto personal que pertenezca al objetivo o residuos corporales recientes(un mechón de pelo, trozos de uñas cortadas, sangre, etcétera).",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_05, USAttributes.None)
		{
			Tittle = "HECHIZO: Elementalismo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Invocas los elementos para golpear a tus enemigos. Gasta 1 punto para lanzar un ataque usando tu magia como arma(3 - daño, cerca o 2 - daño, cerca, área).",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_06, USAttributes.None)
		{
			Tittle = "HECHIZO: Borrar la memoria",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Gasta 1 punto para hacer que un objetivo indefenso olvide hasta una hora de su memoria reciente.Puedes gastar un punto más y marcarte corrupción para colocar recuerdos alternativos en su lugar.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_07, USAttributes.None)
		{
			Tittle = "HECHIZO: Escudo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Gasta 1 punto para obtener armadura+1 o dársela a alguien cercano, o gasta 2 puntos para proporcionar armadura+1 a todas las personas que haya en una zona pequeña, entre las que puedes estar incluido.Esta armadura dura hasta el final de la escena.Puedes apilar varios usos de Escudo a la vez.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_08, USAttributes.None)
		{
			Tittle = "HECHIZO: Manto de oscuridad",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Gasta 1 punto para hacerte invisible durante unos momentos.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_09, USAttributes.None)
		{
			Tittle = "HECHIZO: Teletransporte",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Gasta 1 punto para teletransportarte una corta distancia dentro de la escena en la que te encuentras.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Mage_10, USAttributes.None)
		{
			Tittle = "HECHIZO: Maleficio",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "asta 1 punto para hacerle 1 - daño(perforante) a alguien a cualquier distancia.Para ello necesitas una muestra de su pelo, sangre o saliva.",
			}
		});

		return result;
	}
	private List<USMove> GenerateOracleMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Orac_01, USAttributes.Soul)
		{
			Tittle = "Predicciones",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Al inicio de cada sesión, tira con Espíritu. Con un 10+, obtienes 2 puntos. Con un 7-9, obtienes 1 punto. Durante la sesión, puedes gastar cada uno de esos puntos para declarar que algo terrible está a punto de pasar. Tus aliados y tú obtenéis un +1 a todas las tiradas destinadas a evitar el inminente desastre. Si fallas la tirada, ves la muerte de alguien importante para ti y obtienes un –1 a todas las tiradas destinadas a impedirlo.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Orac_02, USAttributes.Soul)
		{
			Tittle = "Psicometría",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Siempre que examines y analices un objeto interesante, tira con Espíritu. Con un 10+, haz 3 preguntas. Con un 7-9, haz 1 pregunta:",
				Options = new List<string>
					{
						"¿Cuál es la historia de este objeto?",
						"¿Qué maldiciones, protecciones o límites tiene este objeto?",
						"¿Cuál es el lugar de este objeto?",
						"¿Qué secretos o misterios ha presenciado este objeto?",
						"¿Qué emociones fuertes han estado cerca de este objeto últimamente?"
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si fallas, la emoción que hay en el objeto te abruma y obtienes –1 a todas las tiradas durante esa escena."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Orac_03, USAttributes.None)
		{
			Tittle = "Doble vida",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Toma Mortalidad como segunda Facción. Cuando alguien tire con tu Facción o se la marque,  dile cuál de las dos que tienes es más adecuada.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Orac_04, USAttributes.None)
		{
			Tittle = "Médium",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Avanza liberar tu poder para todos los personajes que haya en tu presencia, incluido tú mismo.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Orac_05, USAttributes.None)
		{
			Tittle = "Cueste lo que cueste",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando interfieras en los planes o acciones de alguien para impedir que una de tus visiones se cumpla, márcate su Facción y obtendrás +1 a la siguiente tirada.",
			}
		});
		result.Add(new USMove(USMoveIDs.A_Orac_06, USAttributes.None)
		{
			Tittle = "Rozar la superficie",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando toques a alguien, puedes leer sus pensamientos superficiales. Tira con Espíritu.Con un 10 +, haz 3 preguntas.Con un 7 - 9, haz 1 pregunta:",
				Options = new List<string>
					{
						"¿Qué está pensando tu personaje ahora mismo ?",
						"¿A quién estás protegiendo?",
						"¿Por qué guardas secretos?",
						"¿Qué dolor oculta tu personaje?"
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si fallas, haces 1 - daño(perforante) a la otra persona y a ti mismo."
			}
		});

		return result;
	}
	private List<USMove> GenerateCorruptedMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Corrup_01, USAttributes.Blood)
		{
			Tittle = "El demonio interior",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando adoptes tu forma demoníaca, tira con Sangre. Con un 10+, elige 2 opciones. Con un 7-9, elige 1 opción. Si fallas la tirada, elige 1 opción y contraes una Deuda con tu jefe.",
				Options = new List<string>
					{
						"Obtienes armadura+1.",
						"Te curas 2-daño.",
						"Infliges +1 daño.",
						"+arma demoníaca (3-daño, toque; o 2-daño, cerca).",
						"+desplazamiento demoníaco (vuelo, moto llameante, etcétera)."
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Si estás haciendo un trabajo para tu jefe, elige 1 opción más. Si te marcas corrupción, elige 1 opción más."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_02, USAttributes.None)
		{
			Tittle = "Invocación",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Puedes cobrarte una Deuda que alguien tenga contigo para aparecer en su presencia. Los demás también pueden cobrarse una Deuda que tengas con ellos para hacerte aparecer donde estén."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_03, USAttributes.Blood)
		{
			Tittle = "No me mires",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando confundas a alguien, tira con Corazón en vez de Mente."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_04, USAttributes.Soul)
		{
			Tittle = "Zarcillos en la oscuridad",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando busques la orientación de tu jefe mediante rituales y presagios, tira con Espíritu. " +
				"Si superas la tirada, se muestran señales ante ti: si sigues la senda marcada, obtienes un +1 a la siguiente. " +
				"Con un 7-9, acabas aún más involucrado en el servicio a tu jefe; para poder seguir tu camino tendrás que mantener la calma. " +
				"Si fallas la tirada, tu jefe tiene trabajo para ti ahora mismo; adopta tu forma demoníaca y ponte manos a la obra o sufrirás 2-daño (perforante)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_05, USAttributes.None)
		{
			Tittle = "Frío como el hielo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Súmate 1 a Sangre (máximo +3)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_06, USAttributes.None)
		{
			Tittle = "Duro como el acero",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Obtienes 1-armadura. Las fuentes de daño benditas o sagradas ignoran tu armadura. Las armas diseñadas para aturdir o incapacitar no te afectan"
			}
		});
		result.Add(new USMove(USMoveIDs.A_Corrup_07, USAttributes.None)
		{
			Tittle = "Trabajo demoniaco",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando termines un trabajo para tu jefe, márcate Velo. Tu jefe contrae una Deuda contigo por\r\ncada trabajo que termines."
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Puedes cobrarte una Deuda de tu jefe, como a cualquier otro personaje no jugador, para que:",
				Options = new List<string>
					{
						"Responda con sinceridad a una pregunta sobre su Facción.",
						"Te presente a un miembro poderoso de su Facción.",
						"Te haga un regalo valioso y útil.",
						"Cancele una Deuda de la que sea acreedor.",
						"Te transfiera a ti el derecho a cobrarle una Deuda a otra persona.",
						"Te dé un +3 para Convencerlo (esta opción ha de elegirse antes de tirar)."
					}
			},
			ConsequencesOn10 = new Consequences
			{
				MainText = "Puede que tu jefe te ofrezca una oportunidad de comprar tu libertad, pero las Deudas solas\r\nno bastarán."
			}
		});
		return result;
	}
	private List<USMove> GenerateFairyMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Fai_01, USAttributes.None)
		{
			Tittle = "Magia feérica",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Siempre que uses un poder feérico, elige 1 opción:",
				Options = new List<string>
					{
						"Márcate corrupción.",
						"Contraes una Deuda con tu monarca.",
						"Sufres 1-daño (perforante)."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_02, USAttributes.None)
		{
			Tittle = "Un plato que se sirve ahora",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando jures vengar a alguien (incluido tú mismo), obtienes +1 a todas las tiradas contra el objetivo de esa venganza. Por cada escena en la que no persigas tu venganza, sufrirás 1-daño (perforante)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_03, USAttributes.Blood)
		{
			Tittle = "Lo llevamos en la sangre",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando engañes a alguien, tira con Corazón en vez de Mente."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_04, USAttributes.None)
		{
			Tittle = "La balanza de la Justicia",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Puedes cobrarle una Deuda a alguien para usar un poder de Magia feérica (incluidos poderes que normalmente no podrías usar) sobre esa persona sin coste."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_05, USAttributes.None)
		{
			Tittle = "Descorrer el Velo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando escapes, añade esta opción a la lista:",
				Options = new List<string>
					{
						"Escapas a tu tierra natal, para bien o para mal."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_07, USAttributes.None)
		{
			Tittle = "Las palabras se las lleva el viento",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando alguien rompa una promesa que te haya hecho o te mienta y lo descubras, contrae una Deuda contigo y obtienes +1 a la siguiente tirada contra él."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_08, USAttributes.None)
		{
			Tittle = "PODER: Furia salvaje",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Invocas un elemento de la naturaleza con el que puedes golpear a tus enemigos (2-daño, cerca, área; o 3-daño cerca/lejos)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_09, USAttributes.None)
		{
			Tittle = "PODER: La caricia de la Naturaleza",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "El contacto contigo cura 2-daño. No puedes usar este poder en ti mismo."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_10, USAttributes.None)
		{
			Tittle = "PODER: Marchitar",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Haces que el contacto contigo resulte mortífero (3-daño, íntimo, perforante)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_11, USAttributes.None)
		{
			Tittle = "PODER: Glamour",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Creas ilusiones para engañar los sentidos. Los efectos no duran mucho."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_12, USAttributes.None)
		{
			Tittle = "PODER: Cambio de forma",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Puedes tomar forma de animal brevemente."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Fai_13, USAttributes.None)
		{
			Tittle = "PODER: Confusión",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Toca a un objetivo para sumirlo en un estado emocional concreto (a tu elección). Puedes marcarte corrupción para elegir hacia quién está dirigida esa emoción."
			}
		});

		return result;
	}
	private List<USMove> GenerateSpectreMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Spe_01, USAttributes.None)
		{
			Tittle = "Manifestarse",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "La gente normal no puede percibirte ni interactuar contigo a menos que te manifiestes. Para manifestarte, dedica un momento a concentrarte con tranquilidad y elige 2:",
				Options = new List<string>
					{
						"Eres audible.",
						"Eres visible.",
						"Eres tangible para el mundo físico y este es tangible para ti."
					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Puedes marcarte corrupción para elegir solo 1 opción o las 3."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Spe_02, USAttributes.None)
		{
			Tittle = "No toleraré que me ignores",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Cuando fastidies a alguien, no tires y considera que has sacado un 10+. Si distraes a un personaje no jugador, tira con Espíritu en vez de Mente."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Spe_03, USAttributes.None)
		{
			Tittle = "Ciudad fantasma",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Cuando te eches a la calle en busca de fantasmas, obtendrás un +1 a todas las tiradas para tratar con ellos."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Spe_04, USAttributes.None)
		{
			Tittle = "Fantasmagoría",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Obtienes +1 a Espíritu (máximo +3)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Spe_05, USAttributes.None)
		{
			Tittle = "Cuando crees que me ves",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Siempre tienes una oportunidad para escapar de una situación. Puedes elegir una opción más de la lista para llevarte a alguien contigo. Si fallas, atraerás la atención de espíritus y fantasmas peligrosos que haya por la zona."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Spe_06, USAttributes.None)
		{
			Tittle = "Vínculo",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Algo te impide descansar en paz: un Vínculo. Cuando estés en presencia de tu Vínculo, avanza liberar tu poder. Cuando tu Vínculo esté en peligro, tendrás acceso a todos tus movimientos de corrupción hasta que esté a salvo. Si en algún momento tu Vínculo se destruye, tú también te destruirás."
			}
		});

		return result;
	}
	private List<USMove> GenerateSwornMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Swo_01, USAttributes.Mind)
		{
			Tittle = "Proteger y servir",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando leas una situación cargada, tira con Mente. Con un éxito, haz preguntas al MC; toma +1 cuando actúes según las respuestas",
				Options = new List<string>
					{
						"¿cuál es mi ruta de escape / entrada / salida?",
						"¿qué enemigo es más vulnerable a mí?",
						"¿qué debo vigilar?",
						"¿Cuál es la verdadera posición de mi enemigo?",
						"¿en quién no puedo confiar?",

					}
			},
			ConsequencesOn79 = new Consequences
			{
				MainText = "Con 7-9, pregunta 1."
			},
			ConsequencesOn10 = new Consequences { MainText = "Con un 10+, pregunta 2." },
			ConsequencesOn6 = new Consequences { MainText= "Con un fallo, reconoces una debilidad en tu propia posición o preparativos que deberías haber visto venir." }
		}) ;
		result.Add(new USMove(USMoveIDs.A_Swo_02, USAttributes.Mind)
		{
			Tittle = "Difícil de esquivar",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando sigues a un PNJ por las calles de la ciudad, tira con Mente. Si aciertas, a donde ellos vayan, tú los sigues."
			},
			ConsequencesOn79 = new Consequences { MainText = "Con un 7-9, te encuentras con algún problema por el camino; resuélvelo rápidamente o pierde el rastro" },
			ConsequencesOn6 = new Consequences { MainText = "Con un fallo, tu presa te lleva exactamente donde quiere; prepárate para las fauces cerradas de la trampa." }
		});
		result.Add(new USMove(USMoveIDs.A_Swo_03, USAttributes.None)
		{
			Tittle = "Enrevesado",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Coge +1 Mente (máx+3)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Swo_04, USAttributes.Mind)
		{
			Tittle = "Aunténtico policía",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando pongas cara a un nombre o estudies un santuario, punto de reunión o lugar de poder, tira con Mente en lugar de con el Círculo correspondiente. Siempre puedes hacer una pregunta adicional al MC sobre la persona o lugar en cuestión, incluso si fallas."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Swo_05, USAttributes.None)
		{
			Tittle = "Ajedrez, no damas",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando recurras a la violencia con cualquier tipo de ventaja seria -números, posición, sorpresa, etc.- y consigas un éxito, dile a tu oponente qué opción no puede elegir de su lista antes de que elija."
			}
		});

		return result;
	}
	private List<USMove> GenerateImpMovements()
	{
		var result = new List<USMove>();

		result.Add(new USMove(USMoveIDs.A_Imp_01, USAttributes.Mind)
		{
			Tittle = "Como de costumbre",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando pase el tiempo -o al principio de la partida- tira con Mente. Con un acierto, tus operaciones habituales generan un nuevo plan o proporcionan una oportunidad para avanzar en uno de tus planes existentes, tú eliges.",
				Options = new List<string>
					{
						"¿cuál es mi ruta de escape / entrada / salida?",
						"¿qué enemigo es más vulnerable a mí?",
						"¿qué debo vigilar?",
						"¿Cuál es la verdadera posición de mi enemigo?",
						"¿en quién no puedo confiar?",

					}
			},
			ConsequencesOn10 = new Consequences { 
				MainText = "Con un 10+, también eliges 1:",
				Options = new List<string>
					{
						"- Un cliente leal revela los secretos de un poderoso PNJ, a tu elección.",
						"Un PNJ que te debe una deuda aparece para cumplir con su obligación.",
						"Un PNJ de estatus 3 de tu círculo te ofrece una deuda por tus servicios.",
					}
			},
			ConsequencesOn6 = new Consequences { MainText = "Si fallas, un familiar o amigo íntimo te arrastra a un plan que preferirías haber evitado; genera un nuevo plan con tres complicaciones y el MC te dirá qué terrible destino le espera a tu aliado si no cumples." }
		});
		result.Add(new USMove(USMoveIDs.A_Imp_02, USAttributes.Mind)
		{
			Tittle = "Mide tu marca",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando loas a alguien, añade las siguientes preguntas a la lista:",
				Options = new List<string>
				{
					"¿Qué necesidad apremiante tienes que yo podría resolver?",
					"¿Qué es lo más valioso que ofrecería en venta?"
				}
			},
			ConsequencesOn6 = new Consequences { MainText = "Si fallas, pregunta 1 de esta lista, pero parecerás sospechoso o sórdido, tú eliges." }
		});
		result.Add(new USMove(USMoveIDs.A_Imp_03, USAttributes.None)
		{
			Tittle = "Enrevesado",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Coge +1 Mente (máx+3)."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Imp_04, USAttributes.Mind)
		{
			Tittle = "Amigos en los bajos fondos",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando consigas un acierto al poner cara a un nombre con un PNJ de estatus 3, nombra también a un subordinado o ayudante de bajo nivel que trabaje para ellos y describe cómo este subordinado ha llegado recientemente a tener una deuda contigo."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Imp_05, USAttributes.None)
		{
			Tittle = "Soy un puto demonio",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Ignora todo el daño la primera vez que alguien te inflija al menos 2 de daño en una escena. Al final de cada escena, borra tu casilla de Daño leve."
			}
		});
		result.Add(new USMove(USMoveIDs.A_Imp_06, USAttributes.Mind)
		{
			Tittle = "Palabras de comadreja",
			TypeOfMovement = USMove.MovementTypes.ArquetipeMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando te niegues a cumplir una Deuda por la vía rápida, tira con Mente en lugar de con la diferencia de Estatus. Si aciertas -además de los efectos normales- marca el Círculo de tu deudor como si hubieras cumplido la Deuda."
			}
		});

		return result;
	}


	private List<USMove> GenerateHunterCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Hun_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Divide y venceré",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando entres solo en una situación peligrosa, marca corrupción para adelantar todos tus movimientos y lleva +1 a Sangre por la escena.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Hun_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Difícil de matar",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para ganar armadura+1 hasta el final de la escena.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Hun_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Esperando ayuda",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para que un equipo de apoyo de cazadores mortales llegue a la escena (3-daño pequeño 1-armadura entrenados) Marca una segunda corrupción para que aparezcan en una posición superior.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Hun_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Impulso suicida",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Si hay alguien cerca de ti a punto de sufrir daño, puedes marcarte corrupción para sufrirlo tú en su lugar.",
			}
		});
		return result;
	}
	private List<USMove> GenerateAwakenCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Awa_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Metido hasta el cuello",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para interponerte en el camino de alguien de otro Círculo como si hubieras sacado un 10+.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Awa_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Si no puedes con ellos…",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Coge una habilidad (Liberar tu poder) de un arquetipo de otro Círculo. Siempre que la sueltes y saques un 12+, marca una corrupción adicional.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Awa_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Libre como el viento",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para negarte a pagarle una Deuda a alguien que no sea mortal como si hubieras sacado un 12+.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Awa_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Manos largas",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción después de reunirte con un PNJ poderoso para revelar que le has quitado algo importante. Marca corrupción de nuevo para ocultar tu papel en el robo durante algún tiempo.",
			}
		});
		return result;
	}
	private List<USMove> GenerateVeteranCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Vet_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "De vuelta a las andadas",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Coge dos habilidades de 'Liberar tu poder' de otro arquetipo. Cuando uses estas habilidades no puedes elegir no marcar corrupción (con 10+)",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vet_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Síndrome de Diógenes",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para buscar entre tus cosas y encontrar justo lo que necesitas para lidiar con la situación en la que te encuentras.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vet_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "¿Os pillo en mal momento, cabrones?",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para entrar en escena. Puedes marcártela una vez más para traerte a alguien contigo.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vet_04, USAttributes.Mind)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Horribles experimentos",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando trabajas en tu taller con alguien (vivo o muerto) marca corrupción para hacer hasta dos preguntas sobre sus secretos o debilidades. Deben responder honestamente.",
			}
		});
		return result;
	}
	private List<USMove> GenerateSpectreCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Spe_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Posesión",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para poseer mentalmente a una persona débil (a discreción del MC) en tu presencia; borra un trauma por cada experiencia humana \"normal\" -comer una comida, comprar ropa, etc.- que realices mientras controlas su cuerpo.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Spe_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Telequinesia",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Puedes mover y levantar objetos pequeños a distancia si te concentras. Puedes marcarte corrupción para mover objetos mayores, pero de un tamaño no superior al de un coche.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Spe_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Pesadilla",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Márcate corrupción para meterte en los sueños de alguien que esté durmiendo en tu presencia. Mientras estés allí, puedes interactuar con esa persona y con sus sueños como si se tratasen de espíritus.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Spe_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Sifón",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Márcate corrupción para introducirte en el cuerpo de alguien, hacerle 2-daño (perforante) y curar 1-daño de tu corpus.",
			}
		});
		return result;
	}
	private List<USMove> GenerateWolfCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Wolf_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Uno con la bestia",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción cuando te transformes para seleccionar dos cualidades adicionales o eliminar dos debilidades existentes de su Transformación. Marca una segunda corrupción para hacer ambas cosas.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Wolf_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "La fuerza de la naturaleza",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Obtienes +1 Sangre (máx. +4). Cada vez que tiras con Sangre y obtienes un 12+, marca corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Wolf_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Sabueso callejero",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para transformarte en un coyote o un perro. Mientras estás en esta forma, puedes tirar con ESPÍRITU en lugar de mente para leer a alguien o engañarlo, distraerlo y engañarlo.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Wolf_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Territorio conocido",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para ubicar la fuente del mayor peligro para ti o alguien que selecciones dentro de tu territorio o centro de la ciudad, incluso si la amenaza se ha ocultado con magia o mentiras.",
			}
		});
		return result;
	}
	private List<USMove> GenerateVampireCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Vamp_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Auténtico cazador",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando estés persiguiendo a un personaje no jugador humano por la noche, márcate corrupción. Tu presa no podrá escapar de ti, huya adonde huya, y podrás alimentarte de él o matarlo a voluntad.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vamp_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Mantenlos dentro",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando cobres la última Deuda de alguien de tu Web, marca corrupción para conservar la Deuda y mantenerlos en tu Red.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vamp_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Fake news",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando debilitas la posición de alguien mediante rumores falsos, marca corrupción para tirar con Corazón en lugar de Estatus. Si fallas, marca corrupción para que el rastro conduzca a un aliado, no a ti.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Vamp_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Magia de sangre",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Elige 2 Poderes de Hada; marca corrupción para usar uno sin coste adicional. Puedes realizar este avance de corrupción una segunda vez para obtener los Poderes de Hada restantes.",
			}
		});
		return result;
	}
	private List<USMove> GenerateMageCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Mage_01, USAttributes.Soul)
		{
			Tittle = "Las Artes Oscuras",
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando recurras a la violencia con energías mágicas o psíquicas, marca corrupción para tirar con Espíritu en lugar de Sangre.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Mage_02, USAttributes.None)
		{
			Tittle = "Sobre un caballo pálido",
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción, 1 por 1, y pronuncia el verdadero nombre de un personaje en la escena para infligir hasta 3 daños (ap) en ellos; solo puedes apuntar a un personaje con este poder una vez por sesión.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Mage_03, USAttributes.None)
		{
			Tittle = "Magia negra",
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para ignorar un requisito que te haya puesto el Maestro de Ceremonias al usar tu santuario.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Mage_04, USAttributes.None)
		{
			Tittle = "Protección",
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para crear una protección mágica del tamaño de una habitación pequeña. La protección dura un mes y un día o hasta que la liberes.",
			}
		});
		return result;
	}
	private List<USMove> GenerateOracleCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Orac_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Émpata",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando cales a alguien, Roces la superficie o utilices Psicometría, puedes marcarte corrupción para hacer las preguntas que quieras, no solo las de las listas.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Orac_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "El ojo que todo lo ve",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción y sufrir 1-daño (perforante) para tener una visión sobre la situación en la que te encuentras. Hazle una pregunta al Maestro de Ceremonias; la contestará con honestidad.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Orac_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Oscuro destino",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para maldecir a una facción de la ciudad con una suerte terrible; tendrán -1 en curso en el siguiente turno de facción. Marca corrupción de nuevo para ocultar tu papel o asegurarte de que la maldición dure mucho tiempo.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Orac_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Mirada penetrante",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para clavar los ojos en alguien y obligarle a quedarse quieto mientras mantengas la mirada. Vuelve a marcar Corrupción para hacerles olvidar la experiencia.",
			}
		});
		return result;
	}
	private List<USMove> GenerateCorrupteCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Corrupt_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Beneficios adicionales",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para hacer un movimiento de ciudad adicional cuando pasa el tiempo; si usas tu Estatus de Círculo para el movimiento, añade +1 a tu tirada.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Corrupt_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Justo bajo la superficie",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Puedes marcarte corrupción para adoptar tu forma demoníaca sin tirar y con todas las opciones de la lista.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Corrupt_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Innegable",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando alguien se niegue a pagarte una Deuda, puedes marcarte corrupción para convertir su resultado en un fallo (tras su tirada).",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Corrupt_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Desde el Infierno",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Márcate corrupción para hacer que tu jefe mande una cuadrilla de demonios a trabajar para ti durante una escena (2-daño, grupo pequeño, 2-armadura, demoníacos).",
			}
		});
		return result;
	}
	private List<USMove> GeneratefairyCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Fai_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Aire y oscuridad",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Obtienes los poderes feéricos restantes. Cuando uses Magia feérica, dejas de poder elegir sufrir daño.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Fai_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Negociador astuto",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando te niegas a cumplir una Deuda, puedes marcar corrupción para sacar un 12+ en vez de tirar.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Fai_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Gracia supranatural",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Obtienes +1 a Corazón (máximo +4). Siempre que tires con Corazón y saques un 12+, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Fai_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Todos tenemos una",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Toca a alguien y marca corrupción para maldecirlo con una vulnerabilidad elemental. Todo el daño de una fuente que selecciones (fuego, acero, hierro, etc.) se trata como +1 daño y ap.",
			}
		});
		return result;
	}
	private List<USMove> GenerateImpCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Imp_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Así es como gano yo",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando avises a tu Círculo de que necesitas algo, marca corrupción para sacar un 12+ en lugar de tirar el dado. Marca la corrupción para dar al vendedor una Deuda en lugar del precio que te pidan cuando llegue el objeto.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Imp_02, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Dinero sucio",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando completes un plan, marca corrupción para recibir las cuatro bendiciones en lugar de sólo dos.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Imp_03, USAttributes.Heart)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Endulza el trato",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando persuades a un PNJ ofreciéndole una bonificación adicional o un soborno atractivo, marca corrupción para sacar un 12+ en lugar de tirar el dado.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Imp_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "En la lista negra",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para declarar a alguien enemigo de tu pueblo; otros de los tuyos lo gasearán, antagonizarán o algo peor. Hasta que digas lo contrario, avanza despistar, distraer o engañar para cualquiera que se dirija a ellos; también reciben -1 en curso durante cada turno de facción.",
			}
		});
		return result;
	}
	private List<USMove> GenerateSwornCorruptionMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.C_Swo_01, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Un paso por delante",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Obtienes +1 Mente (máx+4). Siempre que tires con Mente y saques un 12+, marca corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Swo_02, USAttributes.Mind)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Chivatos",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Marca corrupción para tirar con Mente en vez de Estado cuando consultes a tus contactos en otro Círculo.",
			},
			ConsequencesOn6 = new Consequences { MainText = "Si fallas, marca corrupción de nuevo y elige entre responder a la pregunta difícil de tu contacto o deberle una Deuda." }
		}) ;
		result.Add(new USMove(USMoveIDs.C_Swo_03, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Estudiante de las Artes Arcanas",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Elige tres Hechizos. Marca corrupción para ganar dos puntos que puedes usar para lanzar esos hechizos.",
			}
		});
		result.Add(new USMove(USMoveIDs.C_Swo_04, USAttributes.None)
		{
			TypeOfMovement = USMove.MovementTypes.CorruptionMovement,
			Tittle = "Asuntos Infernales",
			IsSelected = false,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando pases a la violencia, puedes marcar corrupción para sacar un 12+ en vez de tirar.",
			}
		});
		return result;
	}


	private List<USMove> GenerateHunterDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Hun_01, USAttributes.None)
		{
			Tittle = "No matarás",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando hieras a un mortal durante tu persecución de lo sobrenatural, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Hun_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, hazle\r\nuna pregunta; tendrá que contestarla con honestidad. Luego te hará una pregunta a ti; contéstala\r\ncon honestidad o márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Hun_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Hunter,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje, elije a un personaje de otro jugador y dale uno de los\r\nmovimientos de Cazador que tengas. Se lo puede quedar para siempre.",
			}
		});
		return result;
	}
	private List<USMove> GenerateAwakenDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Awak_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando des de lado tus responsabilidades mortales para ocuparte de lo sobrenatural, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Awak_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con alguien que no sea mortal, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Awak_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Awaken,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras, cada personaje tiene que decidir si tu muerte le inspira o corrompe una parte de su ser. Si le inspira, se borra un avance de corrupción que haya tomado (en caso de que lo haya hecho). Si le corrompe, toma un avance de corrupción inmediatamente.",
			}
		});
		return result;
	}
	private List<USMove> GenerateVeteranDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Vet_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando te lances hacia el peligro de cabeza y de manera consciente, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vet_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, cuéntale una historia sobre el pasado y las lecciones que has aprendido.",
				Options = new List<string>
					{
						"Ambos obtenéis +1 a la siguiente.",
						"Tú obtienes +1 a la siguiente y la otra persona, –1 a la siguiente.",
						"Obtienes 1 punto. Gástalo para echarle una mano a ese personaje, por muy lejos que esté."
					}
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vet_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Veteran,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje, elije a un personaje para que herede tu taller y Auténtico artista.",
			}
		});
		return result;
	}
	private List<USMove> GenerateSpectreDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Spe_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Cuando presencies una injusticia y no hagas nada,  márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Spe_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, obtendrás 1 punto. Siempre que esa persona se meta en problemas, puedes gastar uno de esos puntos para aparecer donde esté.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Spe_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Spectre,
			PreCondition = new Consequences
			{
				MainText = "Cuando rellenes todas tus casillas de daño, tu corpus quedará esparcido y disperso. Te recompondrás en unos cuantos días. Cuando tu espíritu descanse en paz de manera permanente o retires a tu personaje, todos los personajes presentes obtendrán +1 Espíritu (máximo +3).",
			}
		});
		return result;
	}
	private List<USMove> GenerateWolfDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Wolf_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando empieces una cacería en pos de alguien, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Wolf_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, crearás un vínculo primario con ella; siempre sabrás dónde encontrarla y cuándo está en problemas. Este vínculo durará hasta el final de la siguiente sesión.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Wolf_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Wolf,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje, alguien a quien quieras proteger que esté en escena escapará y se pondrá a salvo, por muy improbable que eso fuera.",
			}
		});
		return result;
	}
	private List<USMove> GenerateVampireDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Vamp_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando te alimentes de una víctima en contra de su voluntad, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vamp_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona,  cuéntale un secreto sobre ti o contraerás una Deuda con ella. De una forma u otra, esa persona entra en tu red y contrae una Deuda contigo.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vamp_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje,  nombra a alguien que haya en escena a quien quieras muerto; tus subordinados y aliados lo perseguirán sin tregua.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vamp_04, USAttributes.None)
		{
			Tittle = "Movimiento inicial",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Al principio de cada sesión, elige a alguien de tu red y descubre un secreto sobre él que preferiría\r\nque no saliera a la luz.\r\nSolo pueden abandonar tu red quienes ya no estén en Deuda contigo."
			}
		});
		result.Add(new USMove(USMoveIDs.D_Vamp_05, USAttributes.None)
		{
			Tittle = "La red del vampiro",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Vampire,
			PreCondition = new Consequences
			{
				MainText = "Cuando alguien acuda a ti para pedirte consejo, información o un favor, o bien amenace tus intereses,\r\nentra en tu red y contrae una Deuda contigo. Cuando alguien esté en tu red, obtienes lo\r\nsiguiente al tratar con él:",
				Options = new List<string>
					{
						"+1 a todas las tiradas para echarle una mano o fastidiar sus esfuerzos.",
						"Añades esta pregunta a calar a alguien: «¿Qué ansía realmente tu personaje?»."
					}
			}
		});
		return result;
	}
	private List<USMove> GenerateMageDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Mage_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando hagas un trato con alguien oscuro y poderoso, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Mage_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con alguien, decide si esa persona te importa o no. Si no te importa, sigue con su vida tal cual. Si te importa, tiene un –1 a todas las tiradas de escapar hasta que tenga un momento de intimidad con otra persona.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Mage_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Mage,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras, puedes echarle una maldición devastadora a alguien que haya cerca. Especifica sus efectos y cómo puede librarse de ella.",
			}
		});
		return result;
	}
	private List<USMove> GenerateOracleDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Orac_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando le digas a alguien una profecía falsa,  márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Orac_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona,  obtendrás una visión clara y específica sobre su futuro. Puedes hacer un máximo de 3 preguntas sobre la visión; márcate corrupción por cada una que hagas.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Orac_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Oracle,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje,  haz una proclamación al mundo que reverberará en los sueños por todo el planeta. Detalla las señales de lo que se cierne. El Maestro de Ceremonias hará que tu profecía se cumpla más pronto que tarde.",
			}
		});
		return result;
	}
	private List<USMove> GenerateCorruptedDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Corrup_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando convenzas a alguien de algo en nombre de tu jefe, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Corrup_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, te transfiere el derecho a cobrar una Deuda que otro tuviera con ella.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Corrup_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Corrupted,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras,  cóbrate todas las Deudas que tenga contigo tu jefe para volver. Si tu jefe no está en Deuda contigo, le pedirá a otra persona que se endeude por ti. Si esa persona se niega, se acabó lo que se daba. Encantados de haberte conocido.",
			}
		});
		return result;
	}
	private List<USMove> GenerateFairyDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Fai_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando rompas una promesa o digas una mentira descarada, márcate corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Fai_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona,  pídele que te haga una promesa. Si se niega o rompe la promesa, contrae 2 Deudas contigo.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Fai_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Fair,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje, elige a un personaje y concédele el favor de tu corte. Puede elegir entre tomar Magia feérica y dos de tus poderes feéricos o avanzar convencer a un personaje no jugador.",
			}
		});
		return result;
	}
	private List<USMove> GenerateSwornDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Swo_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando rompas uno de tus votos o trabajes en contra de los intereses de tus amos: marca corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Swo_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, dile si te importa más que tu juramento. Si dices que sí marca corrupción y ellos obtienen un punto que pueden gastar en cualqueir momento para convocarte en su ubicación.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Swo_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Sworn,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras o retires a tu personaje, ofrece tu arma a otro. Si la acepta, átalos a tres de tus juramentos. Haz que te lo juren.",
			}
		});
		return result;
	}
	private List<USMove> GenerateImpDramaticMovements()
	{
		var result = new List<USMove>();
		result.Add(new USMove(USMoveIDs.D_Imp_01, USAttributes.None)
		{
			Tittle = "Movimiento de corrupción",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando hagas un trato que ponga en peligro a tu família, amigos o comunidad: marca corrupción.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Imp_02, USAttributes.None)
		{
			Tittle = "Movimiento de intimidad",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando compartas un momento de intimidad física o emocional con otra persona, prométele que le conseguiras algo que desee sin pedir nada a cabio. Dale una deuda y toma +1 en curso para conseguir lo que les has prometido.",
			}
		});
		result.Add(new USMove(USMoveIDs.D_Imp_03, USAttributes.None)
		{
			Tittle = "Movimiento final",
			TypeOfMovement = USMove.MovementTypes.DramaticMovement,
			IsSelected = true,
			Arquetipe = AvailableArchetypes.Imp,
			PreCondition = new Consequences
			{
				MainText = "Cuando mueras uno de tus planes llega a buen puerto, pero otra persona cosecha las recompensas. Elige quien se beneficiará de los planes que pusiste en marcha.",
			}
		});
		return result;
	}



}
