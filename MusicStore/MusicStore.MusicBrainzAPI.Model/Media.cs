using RestSharp.Deserializers;

namespace MusicStore.MusicBrainzAPI.Model
{
    public class Media
    {
        [DeserializeAs(Name = "track-count")]
        public string numberOfTracks { get; set; }
    }
}
