using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Kalendra.Pokemite.Runtime.Infrastructure;
using Newtonsoft.Json;
using UnityEngine;

public class PokemonLocalRetriever : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var pkmn = await new PokeApiClientRepoAdapter().GetPkmn(1);
        var serialized = JsonConvert.SerializeObject(pkmn, Formatting.Indented);

        PlayerPrefs.SetString(pkmn.Name, serialized);

        var destination = Application.persistentDataPath + $"/{pkmn.Name}.txt";
        Debug.Log(destination);

        var file = File.Exists(destination)
            ? File.OpenWrite(destination)
            : File.Create(destination);

        var bf = new BinaryFormatter();
        bf.Serialize(file, serialized);
        file.Close();
    }
}