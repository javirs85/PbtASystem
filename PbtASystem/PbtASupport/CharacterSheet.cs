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

	private AvailableArchetypes _archetype;
	public AvailableArchetypes Archetype { get { return _archetype; } 
		set 
		{
			_archetype = value;
			InitArchetypeUnique();
		} 
	}

	public List<USMoveIDs> SelectedArchetypeMoves { get; set; } = new();
	public List<USMoveIDs> SelectedCorruptionMoves { get; set; } = new();
	public List<USMoveIDs> UpgradedMoves { get; set; } = new();

	public string ArchetypeUniqueTittle1 { get; set; } = "";
	public string ArchetypeUniqueTittle2 { get; set; } = "";
	public string ArchetypeUniqueBody1 { get; set; } = "";
	public string ArchetypeUniqueBody2 { get; set; } = "";

	public void InitArchetypeUnique()
	{
		switch (Archetype)
		{
			case AvailableArchetypes.Hunter:
				ArchetypeUniqueTittle1 = "Tu Arsenal";
				ArchetypeUniqueBody1 = "Crea tres armas personalizadas para tus cacerías; elige una base y dos complementos para cada arma.\r\n\r\n*ARMAS A DISTANCIA BASE**\r\n   - Arco (2-arma cerca/lejos recarga)\r\n   - Escopeta (2-arma mano/cerca fuerte\r\n     recarga desordenada)\r\n   - SMG (2-arma cerca autofire loud)\r\n   - Pistola (2 armas mano/cerca fuerte)\r\n   - Rifle (recarga fuerte lejana de 2 armas)\r\n\r\n*Complementos** (elige 2 para cada arma):\r\n   - Silenciada (-alto)\r\n   - Grande (+1 daño)\r\n   - Semiautomática (-recarga)\r\n   - Automática (+disparo automático)\r\n   - Antigua/Ornada (+sentimental)\r\n   - Bendita (+santo)\r\n   - Alta potencia (+1 daño)\r\n   - Puntería (+lejos o +1 daño a distancia)\r\n   - Plateado (+plata)\r\n   - Mágicamente resistente (+hierro frío)\r\n\r\n*ARMAS DE MANO BASE**\r\n   - Báculo (s-daño mano/cerca)\r\n   - Látigo (1 daño cuerpo a cuerpo)\r\n   - Asta (1 daño mano)\r\n   - Cadena (1-daño mano área agotador)\r\n\r\n*Complementos** (elige 2 para cada arma):\r\n   - Hoja (+1 daño)\r\n   - Pesada (+1 daño)\r\n   - Exclusiva (+truco atrapado)\r\n   - Famosa (+reputación)\r\n   - Extensible (+cerrar)\r\n   - Encantado (+anclado)\r\n   - Plateado (+plata)\r\n   - Mágicamente resistente (+hierro frío)\r\n   - Bendito (+santo)\r\n   - Grueso (+s-daño)\r\n\r\n";
				ArchetypeUniqueTittle2 = "Tu sociedad";
				ArchetypeUniqueBody2 = "Perteneces a una sociedad de cazadores, una afiliación de compañeros mortales que acechan presas peligrosas para proteger el mundo mortal de lo sobrenatural. Dile a tu MC que establezca tu sociedad como una facción de tamaño 2 y fuerza 2 dentro de Mortalis.\r\n\r\n*¿CUÁL ES TU PRESA?**\r\n   - vampiros que se alimentan de los  \r\n     débiles y coaccionados\r\n   - demonios que corrompen a los \r\n     buenos y justos\r\n   - ángeles que subyugan la voluntad \r\n     de los fieles\r\n   - fantasmas que atormentan la vida \r\n     de los inocentes\r\n   - magos que abusan de su poder \r\n     y autoridad\r\n\r\n*¿QUÉ SACRIFICASTE PARA UNIRTE?**\r\n   - mi alma, para siempre manchada por \r\n     el ritual mágico\r\n   - mi familia, extirpada para siempre de \r\n     mi vida\r\n   - mi infancia, para siempre perdida por \r\n     mi entrenamiento\r\n   - mi cuerpo, marcado para siempre por \r\n     la ceremonia\r\n\r\n*¿DÓNDE SE REÚNE TU SOCIEDAD?**\r\n   - un lujoso y caro hotel\r\n   - un bar de mala muerte o un antro de  \r\n     juego\r\n   - un sindicato o un salón de veteranos\r\n   - terreno religioso sagrado";
				break;
			case AvailableArchetypes.Awaken:
				ArchetypeUniqueTittle1 = "Relaciones";
				ArchetypeUniqueBody1 = "Mientras tú te mueves entre el mundo de los mortales y el de los sobrenaturales, tus amigos y tu familia están atrapados en las realidades mundanas de la vida cotidiana. Elige 3:\r\n\r\n - Un hermano menor que confía en tí para el transporte y el consejo\r\n - Una pareja leal que espera que llegues a casa a media noche\r\n - Un mejor amigo luchador que siempre está metido en altercados desordenados\r\n - Un jefe exigente que te llama al trabajo a horas intempestivas\r\n - Un padre anciano que siempre sabe cuando le estás mintiendo\r\n - Una expareja dominante que se preocupa constantemente por tí\r\n\r\n\r\nCuando una de tus relaciones mortales llega a su fin por cualquier motivo -te dejan, mueren, abandonan la ciudad, les dices que dejen de contactar contigo, etc.- marca inmediatamente un avance de corrupción. Si la pérdida de una relación mortal hace que retires a tu personaje debido a la corrupción, dile al MC a quién culpas más por la pérdida;\r\ntu personaje perseguirá al responsable como Amenaza hasta que se haga \"justicia\".\r\n\r\nCuando te ocupes de tus relaciones con los mortales durante el turno de facción, no hagas ningún otro movimiento de ciudad y tira con Corazón. Con un acierto, uno de los mortales más cercanos a ti te ofrece una forma de estrechar vuestros lazos; elimina un avance de corrupción si aceptas lo que te proponen. Con un 7-9, aceptar no es tan sencillo; lo que te piden amenaza con exponerles a la parte de tu vida que has mantenido oculta. Con un fallo, tus intentos de arreglar las cosas llegan demasiado tarde; una de tus relaciones mortales finalmente corta lazos contigo de una forma dolorosa y pública";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Veteran:
				ArchetypeUniqueTittle1 = "Tu taller";
				ArchetypeUniqueBody1 = "Tienes un taller que incluye un amplio espacio para tus herramientas y/o suministros. \r\n\r\nElige y subraya 3 características que incluya tu taller:\r\n\r\nelevador de automóviles y herramientas\r\n   - un cuarto oscuro\r\n   - un entorno de cultivo regulado\r\n   - dos o tres ayudantes cualificados\r\n   - un desguace de materias primas\r\n   - herramientas de mecanizado\r\n   - transmisores y receptores\r\n   - un campo de pruebas\r\n   - trampas mortales\r\n   - una biblioteca de libros antiguos\r\n   - una dispersión de reliquias antiguas\r\n   - un foco místico\r\n   - salas mágicas\r\n   - una estación médica\r\n   - un quirófano\r\n   - electrónica y ordenadores de alta tecnología\r\n   - un sistema de vigilancia avanzado, una forja\r\n   - un laboratorio científico\r\n   - un portal a otra dimensión...";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Vampire:
				ArchetypeUniqueTittle1 = "Tu puerto";
				ArchetypeUniqueBody1 = "Tienes un lugar seguro -*un refugio**- a salvo de peligros externos, situado dentro de un *puerto** más grande. Tu refugio es un refugio reforzado con raciones de emergencia y un vector de escape. Cuando alguien entra voluntariamente en tu refugio, lo añades a tu Red.\r\n\r\nTu puerto es un elemento público de la comunidad (elige uno):\r\n\r\n - Un restaurante de gran tamaño\r\n - una estación de transp. público\r\n - un hospital religioso\r\n - un mercado al aire libre\r\n - un club nocturno llamativo\r\n - una biblioteca anticuada\r\n - un teatro restaurado\r\n - un hotel histórico\r\n\r\nTu puerto...(elije 2)\r\n\r\n - es popular entre el común de los mortales, lo que te facilita el acceso a presas desprevenidas.\r\n - alberga una forma de deshacerte rápidamente de los cadáveres, lo que atrae pocas o ninguna sospecha\r\n - está atendido por ghouls leales a ti, que te traen noticias de cualquier cosa que oigan o vean\r\n - te ofrece un espacio para entretener y recibir admiradores, lo que te proporciona un flujo constante de regalos y favores.\r\n - está controlado directamente por un PNJ aliado de Estatus 3 de la Noche, lo que te otorga algunas protecciones limitadas\r\n\r\nY elije 2 más:\r\n - se encuentra dentro del territorio de un señor vampiro del Estatus-3; a menudo exigen tributos y Deudas\r\n - es el hogar de una serie de entidades fantasmales, que van de lo ligeramente molesto a lo profundamente peligroso.\r\n - está estrechamente ligado a varios mortales que te importan; han sido empujados al mundo sobrenatural por tu falta de vida\r\n - ha atraído la atención de un dedicado grupo de cazadores de mortales; pronto podrían hacer su movimiento\r\n - está constantemente ocupado, día y noche; los que lo frecuentan hacen imposible una seguridad estrictat";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Wolf:
				ArchetypeUniqueTittle1 = "Tu territorio";
				ArchetypeUniqueBody1 = "Has reclamado un área de la ciudad como tuya. De forma predeterminada, su territorio cubre una o dos manzanas de la ciudad y tiene el problema: +crimen.\r\nElije2\r\n\r\n\r\n - Tu territorio abarca varias manzanas de la ciudad que has tenido durante años (agrega bendición: + influencia)\r\n - Las personas en tu territorio trabajan duro para mantener las calles seguras (eliminar +crimen)\r\n - Eres ampliamente aceptado como protector de este lugar (añadir bendición: +apoyado)\r\n - Tu territorio incluye terrenos abiertos para que recorras y caces (agrega bendición: +santuario)\r\n - Has hecho un trato con alguien o algo para proteger tu territorio cuando no estás cerca (bendición: +guardian)\r\n\r\n\r\nElije 2\r\n\r\n - Tu territorio le debe lealtad a alguien más poderoso que tú (añade problemas: +obligaciones)\r\n - Un NPC de Estado-3 quiere tu territorio y está trabajando para conseguirlo (agregar problema: +invasión)\r\n - Los mortales en el área están tratando activamente de revitalizar las empresas y la infraestructura locales (agregar problemas: + agitación)\r\n - Tu territorio está plagado de una presencia mística o sobrenatural (añade problemas: +encantado)\r\n - Has ofrecido protección dentro de tu territorio a alguien, y ahora sus problemas son tuyos (agregar problema: +fidelidad)";
				ArchetypeUniqueTittle2 = "Tu Transformación";
				ArchetypeUniqueBody2 = "De forma predeterminada, puedes cambiar a tu forma de lobo, a voluntad, a la vista de la luna: obtienes armamento natural (2-daño), 1-armadura y todas las cualidades y debilidades que elijas a continuación.\r\n\r\n*Elije 2 cualidades**\r\n - Enorme: gana armadura+1 y daño+1\r\n - Salvaje: tu daño es (ap) y escabroso\r\n - Versátil: +1 en curso para dejarlo salir\r\n - Veloz: toma +1 en curso para escapar\r\n - Astuto: +1 para mantener la calma\r\n - Brutal: cuenta como un grupo pequeño\r\n\r\n\r\nElije 2 debilidades:\r\n\r\n - Las armas plateadas ignoran tu armadura e infligen daño+1 \r\n - A veces pierdes el control mientras te transformas \r\n - A veces te transformas cuando estás estresado o enojado. \r\n - La transformación es breve; la pierdes al final de una escena \r\n - La transformación es violenta y dolorosa; sufres 1-daño (ap)\r\n - La transformación llama la atención de las criaturas sobrenaturales.";
				break;
			case AvailableArchetypes.Spectre:
				ArchetypeUniqueTittle1 = "Tus Anclas";
				ArchetypeUniqueBody1 = "Tienes varias anclas en la ciudad: lugares, personas u objetos importantes que te impiden seguir adelante. Puede que tengas la oportunidad de solucionar un ancla, pero las anclas también pueden arruinarse o destruirse.\r\n\r\nELIGE 4:\r\n    un familiar o heredero, inconsciente de tu existencia\r\n    un testigo de tu muerte, unido a ti por el azar\r\n    un animal doméstico amistoso, compañero constante \r\n    una querida posesión de su juventud, transmitida a un \r\n    nuevo propietario\r\n    un símbolo de tu éxito en la vida, reclamado por otro\r\n    un objeto relacionado con tu muerte, que marca \r\n    tu violento final\r\n    un lugar de importe para tí, recuerdo de un amor\r\n    un espacio en el que vivías o trabajabas, abandonado.\r\n\r\nCuando una de tus anclas se pone en peligro, lo sabes; marca el trauma y adopta un +1 continuo a todos los movimientos hasta que la veas a salvo. Cuando resuelves un ancla, despeja tu registro de trauma y borra un avance de corrupción; cuando un ancla es destruida o arruinada, llena tu registro de trauma y toma un avance de corrupción.";
				ArchetypeUniqueTittle2 = "Trauma";
				ArchetypeUniqueBody2 = "Tu sentido del yo ha sido destrozado por tu muerte, dejándote traumatizado. Empiezas cada sesión con 2 traumas marcados, pero puedes eliminarlos -y cualquier trauma adicional que cojas- mediante movimientos de trauma. Si alguna vez llenas tu registro de traumas, el MC puede pedirte que hagas un movimiento de trauma en cualquier momento, pero siempre puedes elegir qué movimiento de trauma haces en ese momento.\r\n\r\nLa primera vez que recibas daño en una escena, marca trauma; cuando te llenes de daño, tu corpus se dispersará. Marca trauma para que se reforme en unos días en una de tus anclas, o marca 3-trauma para que se reforme inmediatamente en un ancla a elección del MC. Si no puedes marcar trauma mientras estás destruido, el CM decide cómo/cuándo te reformas.";
				break;
			case AvailableArchetypes.Sworn:
				ArchetypeUniqueTittle1 = "Juramento";
				ArchetypeUniqueBody1 = "Has jurado servir a una influyente facción del Poder, una organización que confía en ti para proteger a sus miembros, castigar a sus enemigos y defender sus posesiones.\r\n\r\n*Tus maestros**\r\nSirves a (elije 1)\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nEstas al cargo de (elije 2)\r\n\r\n - una abadía de oráculos clarividentes\r\n - un consejo de magos altivos\r\n - una orden de inmortales secretos\r\n - una academia de eruditos religiosos\r\n - un pacto de deidades terrenales\r\n\r\n\r\nEstar al cargo de:(Elije 2)\r\n\r\n\r\n - proteger y vigilar a sus miembros\r\n - recuperar artefactos y tomos perdidos\r\n - investigar amenazas y problemas\r\n - Destruir a los que quieran frustrarlos\r\n - negociar con sus aliados y vasallos\r\n\r\n\r\nDile a tu MC que establezca las estadísticas de tus maestros como una facción de Tamaño-3, Fuerza-3 dentro del Poder. Pregúntales qué sabes sobre la estructura, cultura y activos de la organización.\r\n\r\n*Juramentos**\r\nCuando rompas uno de tus votos, marca corrupción. Cuando hagas un avance de corrupción tacha uno de tus votos. Si tachas todos tus votos tu juramento se ha roto. Cambia de arquetipo inmediatamente. Debes... (elije 5)\r\n\r\n - Jamás traspasar la propiedad de otro\r\n - Nunca dar el primer golpe\r\n - Nunca mentir s. tu identidad/proposito\r\n - Nunca revelar secretos de tu facción\r\n - Siempre evitar daños colaterales\r\n - Siempre proteger miembros de Poder\r\n - Siempre ir a por enemigos de tus jefes\r\n - Siempre mantener que quieren tus jefe";
				ArchetypeUniqueTittle2 = "Tu arma";
				ArchetypeUniqueBody2 = "Tus maestros te han dado un arma (anclada mítica de 3 daños) para que la manejes, un artefacto legendario de una era perdida hace mucho tiempo. Elige una:\r\n\r\n - una espada empuñada por reyes legítimos (descifrar a alguien)\r\n\r\n - una lanza robada del mismísimo cielo (déjala salir)\r\n\r\n - un bastón imbuido de hechicería sigilosa (escapar de una situación)\r\n\r\n - un cuchillo cortado de acero demoníaco (engañar, distraer o engañar)\r\n\r\n - un hacha forjada en la sangre de los dioses (vuelta a la violencia)\r\n\r\n - un martillo bendecido por magia de otro mundo (mantén la calma)\r\n\r\n\r\nMientras lleves este armamento legendario, avanza en el movimiento mencionado; cuando esgrimes el arma al servicio de tus Maestros, puedes tirar con Mente en lugar de Espíritu cuando mantienes la calma. Pero tu arma, como tu juramento,";
				break;
			case AvailableArchetypes.Mage:
				ArchetypeUniqueTittle1 = "Tu santuario";
				ArchetypeUniqueBody1 = "Elige y subraya 4 características de tu Sanctum:\r\nun asistente muy bien informado, un campo de pruebas, trampas explosivas mágicas, una biblioteca de tomos antiguos, una dispersión de reliquias antiguas, una prisión mística, salas mágicas, un portal a otra dimensión, un círculo de enfoque, un boticario\r\n\r\nElija y subraye 2 desventajas de su Sanctum: \r\nestá maldito por un propietario anterior, atrae la atención de otros mundos, contiene muchas sustancias volátiles, muchos conocen su ubicación, siempre carece de una pieza o ingrediente clave, es difícil para usted acceder, contiene secretos que ni siquiera tú conoces.\r\n\r\nCuando ingresas a tu santuario para trabajar en algo, el MC te dirá: \"Claro, no hay problema, pero...\" y luego de 1 a 4 de lo siguiente:\r\n  • Le llevará horas/días/semanas/meses de \r\n     trabajo o tiempo de recuperación\r\n  • Primero tendrás que invocar/construirX\r\n  • Necesitará los servicios de X para completarlo\r\n  • Requiere un ingredient o material raro y costoso\r\n  • Solo funcionará durante un breve período de \r\n    tiempo y es posible que no sea fiable\r\n  • Significará exponer a cualquier persona cercana \r\n    a graves consecuencias\r\n  • Tu santuario carece de X; añadeselo y \r\n     podrás completalo\r\n  • Requerirá una parte de ti mismo o un \r\n     sacrificio comparable paracompleto\r\n  • Debes viajar a________ para completarlo\r\n\r\nEl MC puede combinar cualquier conjunto de requisitos u ofrecer dos conjuntos de costos para la misma tarea. Una vez que se completan los requisitos, el trabajo está terminado. El MC lo iniciará, revelará información o lo que sea necesario ahora que has terminado.";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Oracle:
				ArchetypeUniqueTittle1 = "Tu benefactor";
				ArchetypeUniqueBody1 = "Tienes un benefactor, un poderoso PNJ cuyo destino está entrelazado con el tuyo; entraste a su servicio como resultado de tus visiones proféticas, pero ahora exige cada vez más de ti y de tus poderes. Nombra su Círculo y nombre y elige la profecía que os une, dos puntos fuertes y dos defectos:\r\n\r\n*Nombre:**\r\n*Círculo:**\r\n*Tú profecía:**\r\n\r\n - sólo ellos pueden guiarte hacia las respuestas místicas que buscas\r\n - sólo ellos pueden protegerte de un destino oscuro que has previsto\r\n - sólo ellos pueden destruir a un enemigo maligno único dentro de su propio Círculo\r\n - sólo ellos pueden desempeñar un papel crucial en la guerra contra la oscuridad que se avecina\r\n\r\n*Fortaliezas:**\r\n - son miembros de Status-3 de su Círculo\r\n - dicen la verdad y honran su palabra\r\n - tienen un gran poder sobrenatural\r\n - sus subordinados son disciplinados y leales\r\n\r\n*Defectos:**\r\n\r\n - son terriblemente violentos y crueles\r\n - tienen defensas contra tu vista\r\n - están en guerra con enemigos poderosos\r\n - están locamente enamorados de ti\r\n\r\nPregúntale al MC qué vida te ha proporcionado tu benefactor después de que tomes tus decisiones. Su continua generosidad depende de lo bien que sirvas a sus intereses.\r\n\r\nCuando acudas a tu benefactor en busca de ayuda o recursos, tira con tu Estatus.\r\nCon un acierto, te dan lo que necesitas, siempre que tengas una visión profética de un problema que tengan en ese momento.\r\nCon un 10+, la ayuda que te dan es excepcionalmente útil. Con un fallo, revelan que has pasado por alto algo que perjudicó enormemente su Estatus; están decididos a recordarte su poder sobre ti antes incluso de considerar tu petición.";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Fair:
				ArchetypeUniqueTittle1 = "Tu Corte";
				ArchetypeUniqueBody1 = "Perteneces a una corte de hadas, presidida por un monarca al que has jurado lealtad. Tu monarca tiene 2 Deudas sobre ti; dile al MC qué favores te concedió para ganarse dichas deudas.\r\n\r\nTu corte es ...\r\n - Barroca y formal\r\n - salvaje y revoltosa\r\n - distante y fría\r\n - misteriosa y exótica\r\n\r\n\r\nLa posición de tu monarca está representada por...\r\n\r\n - Una corona imbuida mágicamente con la autoridad real de tu corte\r\n - Un cetro forjado con los elementos de tu corte\r\n - Una aura mágica ocultable incluso a la magia de las hadas\r\n - Un asiento de poder capaz de convocar toda tu corte\r\n\r\n\r\nTu rival es...\r\n\r\n - Un hermano celoso, le debes 1 por su lealtad\r\n - Un antiguo amante, le debes 1 por su amabildiad\r\n - Un antiguo mentor, le debes 1 por su tutela\r\n - Un colega despectivo, le debes 1 por su paciencia";
				ArchetypeUniqueTittle2 = "";
				ArchetypeUniqueBody2 = "";
				break;
			case AvailableArchetypes.Corrupted:
				ArchetypeUniqueTittle1 = "Tu patrón";
				ArchetypeUniqueBody1 = "Tu alma ha caído en manos de un oscuro patrón, un poderoso demonio cuya reputación le precede, pero cuyo verdadero nombre es conocido por pocos. Te han dado condiciones de empleo, acceso a un poder terrible, y una visión aterradora de su verdadera naturaleza.\r\n\r\nElige dos:\r\n\r\n - seducen a todos los que entran en contacto con ellos con galanterías, regalos y vicios\r\n - gobiernan su extensa organización mediante normas y castigos estrictos y severos\r\n - manipulan a sus amigos, aliados y enemigos por igual en conflictos que sirven a sus designios secretos\r\n - han sembrado de ojos y oídos toda la ciudad, siempre alerta a cualquier señal de traición u oportunidad provechosa\r\n - sólo emplean a seguidores leales y entregados; sus enemigos están igualmente entregados a su destrucción\r\n - Tardan en enfurecerse, pero su ira es imparable cuando se les provoca de verdad.\r\n\r\n\r\n\r\n*Tu forma**\r\nDesde que tu patrón reclamó tu alma, tienes un nuevo aspecto: una forma demoníaca. Escoge todas las que correspondan de las listas siguientes:\r\n\r\nCabeza: \r\nhueso, cóncava, corona, llamas, halo, cuernos, pinchos\r\n\r\nOjos: \r\nausentes, animales, vacíos, brillantes, ahumados, sin parpadear\r\n\r\nExtremidades: \r\ngarras, pesadas, con pezuñas, muchas, cortadas, retorcidas\r\n\r\nAlas: \r\nsangre, plumas, insectos, cuero, metal, papel, ninguna\r\n\r\nPiel: \r\nquitinosa, marcada, brumosa, pegajosa, estirada, translúcida";
				ArchetypeUniqueTittle2 = "Trabajos";
				ArchetypeUniqueBody2 = "Tu patrón oscuro te mantiene en la Tierra por una razón; siempre podría haber decidido arrastrarte al infierno. Elige dos de las tareas que realizas habitualmente para ellos de la siguiente lista:\r\n\r\nRecoger almas, rastrear demonios  renegados, entregar amenazas y mensajes, vigilar a alguien o algo, destruir enemigos de tu patrón, negociar contratos demoníacos, esconder y asegurar contrabando demoníaco, operar un establecimiento demoníaco, vigilar a los  esbirros de tu patrón\r\n\r\nCuando completes un trabajo para tu patrón, marca Velo. Tu patrocinador te debe una Deuda por cada trabajo completado. Puedes cobrar una Deuda a tu Patrón para el:\r\n  -Responda a una pregunta onestamente\r\n  -Organice  encuentro con un PNJ de Velo\r\n  -Te conceda una bendició o un regalo útil\r\n  - borre una deuda que tengas con alguien\r\n  - te conceda una deuda que él tenga\r\n\r\nTu patrón tiene 3 deudas contigo. En cualquier momento, tu mecenas puede canjear las Deudas que tengas con él (1 a 1) para infligirte corrupción.";
				break;
			case AvailableArchetypes.Imp:
				ArchetypeUniqueTittle1 = "Tus estafas";
				ArchetypeUniqueBody1 = "Las estafas son negocios arriesgados y estafas turbias diseñadas para ampliar su negocio. Cuando generes un esquema, elige un Círculo principal, uno de tus servicios, y dos complicaciones (el MC te dirá qué oportunidad has aprovechado). Cuando lleves a cabo un un plan, elige dos bonus y un pago; el MC te detallará cómo llegan. Genera una estafa siempre que un bonus o un movimiento te indiquen que generes un nuevo plan.\r\n\r\n*COMPLICACIONES:**\r\n   - prometiste a alguien involucrado algo \r\n     que aún no tienes\r\n   - necesitas un co-conspirador poco \r\n     fiable o de poca confianza\r\n   - debes engañar o embaucar a un PNJ \r\n     poderoso y peligroso\r\n   - necesitas robar algo de un lugar seguro\r\n   - necesitas esperar un momento o \r\n     acontecimiento predeterminado\r\n   - has atraído la atención de una \r\n     oposición peligrosa\r\n\r\n*BONUS:**\r\n   - atraer nuevos negocios; generar un \r\n     nuevo plan\r\n   - pagar tus deudas; cancelar una Deuda \r\n     que tienes\r\n   - hacer valer tu influencia; contraer una \r\n     deuda con un PNJ\r\n   - aumentar tu reputación; marcar un \r\n     Círculo afectado por el trato\r\n\r\n*PAGOS:**\r\nDisponibles al inicio de la partida:\r\n\r\n\r\n - +1 a cualquier círculo (max 3)\r\n - Contratar subordinados diabólicos\r\n - Adquirir un arsenal\r\n - Adquirir una nueva estafa\r\n - Adquirir una nueva estafa\r\n - Resolveer un problemaDespués de 4+ pagos:\r\n\r\n - +1 a cualquier círculo (máx +3)\r\n - +1 Estatus (máx 2)\r\n - Adquiere un arma legendária\r\n - Adquirir un santuario\r\n - Adquirir mágia de hadas\r\n - Retirartu personaje a un lugar seguro";
				ArchetypeUniqueTittle2 = "Tu negocio";
				ArchetypeUniqueBody2 = "Una vez serviste a un poderoso demonio, pero aprovechaste un resquicio legal (un contrato que dio lugar a tu establecimiento) y ganaste tu libertad. Ahora atiendes a clientela sobrenatural de los cuatro Círculos, asegurando tu lugar en este mundo... por ahora. Por defecto, tu establecimiento tiene una ubicación permanente, un personal reducido pero leal y muchos clientes habituales.\r\n\r\nElige dos servicios que ofrezcas:\r\n   - Limpieza de desechos horripilantes o \r\n     ilegales\r\n   - Custodiar o transportar criaturas de \r\n     otro mundo\r\n   - Tasar y vender objetos mágicos\r\n   - Creación de falsificaciones, disfraces e \r\n     identidades robadas\r\n   - Gestionar un lugar de reunión o de poder\r\n\r\nElige dos activos que hayas adquirido a lo largo de los años:\r\n   - Un guardaespaldas o portero experto \r\n     para proporcionar seguridad\r\n   - Un sistema de vigilancia de alta tech y \r\n     una cámara acorazada impenetrable\r\n   - Un lugar móvil u oculto al que sólo tú \r\n     controlas el acceso\r\n   - Una zona muerta mágica asegurada por \r\n     guardianes y rituales\r\n   - Un asiduo poderoso y leal en otro Círculo\r\n\r\nElige dos problemas que aquejen a tu negocio:\r\n   - Un competidor peligroso que se acerca \r\n     a tu mercado\r\n   - Una adquisición difícil de mover que \r\n     atrae una atención no deseada\r\n   - Una reputación mancillada que \r\n     complica cualquier nuevo trato\r\n   - Un antiguo cliente demoníaco que \r\n     busca  su regreso inmediato\r\n   - Un grave estado de deterioro que pone    \r\n     en peligro a su personal";
				break;
			default:
				break;
		}
	}

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
