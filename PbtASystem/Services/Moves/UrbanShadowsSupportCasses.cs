namespace PbtASystem.Services.Moves;

public enum USAttributes
{
	Blood, Heart, Mind, Soul, Mortality, Night, Power, Veil, Circle, None
}

public enum AvailableArchetypes
{
	NotSet, Hunter, Awaken, Veteran, Vampire, Wolf, Spectre, Sworn, Mage, Oracle, Fair, Corrupted, Imp, All
}

public enum LabelIDs
{
	NotSet, Area, Aturdidora, Automática, Cerca, Escabrosa, Fuego, Íntima, Lejos, Ocultable, Perforante, Plata, HierroFrío,
	Sagrada, Recarga, Ruidosa, Toque, Toque_Cerca, Valiosa, daño1, daño2, daño3, daño4,
	Silenciada, Grande, Semiautomatica, Antigua, Potente, ConMira, Bendita, Cabeza, Hoja, Famosa, Extensible, Encantada, Escondida
};

public class Label
{
	public LabelIDs ID { get; set; }
	public string Name
	{
		get
		{
			if (ID == LabelIDs.Toque_Cerca) return "Toque/Cerca";
			else return ID.ToString();
		}
	}
	public string Description
	{
		get
		{
			string text = "";
			switch (ID)
			{
				case LabelIDs.Silenciada:
					text = "Quita ruidosa";
					break;
				case LabelIDs.Grande:
					text = "+1 daño";
					break;
				case LabelIDs.Semiautomatica:
					text = "quita recarga";
					break;
				case LabelIDs.Antigua:
					text = "antigua ornamentada y valiosa";
					break;
				case LabelIDs.Potente:
					text = "+1 daño";
					break;
				case LabelIDs.ConMira:
					text = "añade Lejos, +1 daño si ya tiene lejos";
					break;
				case LabelIDs.Bendita:
					text = "Sagrada, perforante para demonios";
					break;
				case LabelIDs.Cabeza:
					text = "+1 daño";
					break;
				case LabelIDs.Hoja:
					text = "+1 daño";
					break;
				case LabelIDs.Famosa:
					text = "Tiene reputación y puede ser reconocida y valiosa";
					break;
				case LabelIDs.Extensible:
					text = "añade cerca";
					break;
				case LabelIDs.Encantada:
					text = "El arma re-aparece en tu posesión si es lanzada";
					break;
				case LabelIDs.Escondida:
					text = "Añade ocultable";
					break;
				case LabelIDs.NotSet:
					text = "not set yet.";
					break;
				case LabelIDs.Area:
					text = "El ataque de esta arma afecta a un área. Cuando se usa contra un grupo, se ignora su tamaño a la hora de determinar el daño infligido, siempre que los miembros del grupo estén lo bastante juntos.";
					break;
				case LabelIDs.Aturdidora:
					text = "En vez de infligir daño normal, las armas aturdidoras incapacitan a sus objetivos. Puede que los personajes contra los que se use un arma aturdidora tengan que @KeepCalm para mantenerse en pie.";
					break;
				case LabelIDs.Automática:
					text = "Esta arma puede usarse como si fuera +área, pero eso le agota la munición, por lo que será necesario que el personaje recargue.";
					break;
				case LabelIDs.Cerca:
					text = "Esta arma solo se puede usar contra alguien que esté cerca de ti, entre 2 y 10 metros.";
					break;
				case LabelIDs.Escabrosa:
					text = "Esta arma hace heridas graves y sangrientas o destruye lo que haya alrededor del objetivo. Estas armas no son aptas para trabajos de precisión.";
					break;
				case LabelIDs.Fuego:
					text = "Esta arma se basa en el fuego. Prenderá los materiales inflamables que haya en las cercanías y causará quemaduras graves a todos los objetivos con los que entre en contacto. Puede que las criaturas sobrenaturales que sean vulnerables al fuego sufran daño extra o huyan cuando se use una de estas armas contra ellas.";
					break;
				case LabelIDs.Íntima:
					text = "Esta arma solo se puede usar a una distancia cercana y personal, menor que la de un arma +toque, con una longitud no superior a la de tu brazo.";
					break;
				case LabelIDs.Lejos:
					text = "Esta arma solo se puede usar contra oponentes que estén alejados, a más de diez metros; a menos distancia es demasiado difícil de manejar para que tenga efecto.";
					break;
				case LabelIDs.Ocultable:
					text = "Esta arma es fácil de ocultar a la vista, ya que es lo bastante pequeña para caber en el bolsillo de una chaqueta.";
					break;
				case LabelIDs.Perforante:
					text = "Atraviesa la armadura; esta arma ignora la puntuación de armadura del oponente e inflige el daño completo.";
					break;
				case LabelIDs.Plata:
					text = "Esta arma está hecha de un material especial o ha sido bendecida por una persona de gran fe. Las criaturas sobrenaturales pueden ser especialmente vulnerables a estas armas, por lo que el arma puede tener #perforante contra ellas o hacerles +1 daño.";
					break;
				case LabelIDs.HierroFrío:
					text = "Esta arma está hecha de un material especial o ha sido bendecida por una persona de gran fe. Las criaturas sobrenaturales pueden ser especialmente vulnerables a estas armas, por lo que el arma puede tener #perforante contra ellas o hacerles +1 daño.";
					break;
				case LabelIDs.Sagrada:
					text = "Esta arma está hecha de un material especial o ha sido bendecida por una persona de gran fe. Las criaturas sobrenaturales pueden ser especialmente vulnerables a estas armas, por lo que el arma puede tener #perforante contra ellas o hacerles +1 daño.";
					break;
				case LabelIDs.Recarga:
					text = "Esta arma tiene munición limitada y cuando se usa hace falta recargarla a menudo.";
					break;
				case LabelIDs.Ruidosa:
					text = "Todo aquel que esté cerca oye el ruido del arma y puede, hipotéticamente, identificar cuál es su origen. Despierta a quienes estuvieran dormidos, sobre-salta a los que no se lo esperaran, etcétera.";
					break;
				case LabelIDs.Toque:
					text = "Esta arma se puede usar contra alguien que se encuentre dentro de una distancia de un metro o así; en general, lo que abarque la longitud de tu brazo más el arma.";
					break;
				case LabelIDs.Toque_Cerca:
					text = "Se considera que esta arma es capaz de afectar a objetivos que estén a distancia de toque o cerca.";
					break;
				case LabelIDs.Valiosa:
					text = "Este objeto es caro y poco común; puede ser un arma funcional, pero da precedencia al estilo antes que a la sustancia.";
					break;
				case LabelIDs.daño1:
					text = "Hace 1 punto de daño a un oponente antes de aplicar la armadura.";
					break;
				case LabelIDs.daño2:
					text = "Hace 2 puntos de daño a un oponente antes de aplicar la armadura.";
					break;
				case LabelIDs.daño3:
					text = "Hace 3 puntos de daño a un oponente antes de aplicar la armadura.";
					break;
				case LabelIDs.daño4:
					text = "Hace 4 puntos de daño a un oponente antes de aplicar la armadura.";
					break;
				default:
					text = "label not found";
					break;
			}
			return text;
		}
	}
}

public class Item
{
	public string Name { get; set; } = "";
}

public class Weapon : Item
{
	public int Damage
	{
		get
		{
			int dmg = 0;
			if (Labels.Contains(LabelIDs.daño1)) dmg = 1;
			else if (Labels.Contains(LabelIDs.daño2)) dmg = 2;
			else if (Labels.Contains(LabelIDs.daño3)) dmg = 3;
			else if (Labels.Contains(LabelIDs.daño4)) dmg = 4;

			if (Labels.Contains(LabelIDs.Grande)) dmg++;
			if (Labels.Contains(LabelIDs.Potente)) dmg++;
			if (Labels.Contains(LabelIDs.Cabeza)) dmg++;
			if (Labels.Contains(LabelIDs.Hoja)) dmg++;
			if (Labels.Contains(LabelIDs.Lejos) && Labels.Contains(LabelIDs.ConMira)) dmg++;

			return dmg;
		}
	}
	public List<LabelIDs> Labels { get; set; }
	public Weapon()
	{
		Name = "";
		Labels = new();
	}
}

public static class EnumExtensions
{
	public static string ToUI(this USAttributes ch)
	{
		return ch switch
		{
			USAttributes.Blood => "Sangre",
			USAttributes.Mind => "Mente",
			USAttributes.Soul => "Espíritu",
			USAttributes.Heart => "Corazón",
			USAttributes.Mortality => "Mortalis",
			USAttributes.Night => "Noche",
			USAttributes.Veil => "Velo",
			USAttributes.Power => "Poder",
			USAttributes.Circle => "Círculo",
			_ => $"Unknown characteristic ToUI {ch}"
		};
	}

	public static string ToUI(this AvailableArchetypes arq)
	{
		return arq switch
		{
			AvailableArchetypes.Hunter => "Cazador",
			AvailableArchetypes.Awaken => "Despertado",
			AvailableArchetypes.Fair => "Hada",
			AvailableArchetypes.Mage => "Mago",
			AvailableArchetypes.Veteran => "Veterano",
			AvailableArchetypes.Vampire => "Vampiro",
			AvailableArchetypes.Corrupted => "Corrompido",
			AvailableArchetypes.Oracle => "Oraculo",
			AvailableArchetypes.Wolf => "Hombre Lobo",
			AvailableArchetypes.Spectre => "Espectro",
			AvailableArchetypes.Imp => "Duencedillo",
			AvailableArchetypes.Sworn => "Juramentado",
			_ => $"Unknown arquetipe ToUI: {arq.ToString()}"
		};
	}
}