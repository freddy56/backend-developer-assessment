using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.MusicBrainzAPI.Model
{
    public class Release
    {
        public string id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public List<Media> media { get; set; }
        [DeserializeAs(Name = "artist-credit")]
        public List<OtherArtists> otherArtists { get; set; }
        [DeserializeAs(Name = "label-info")]
        public List<LabelInfo> labelInfo { get; set; }
    }
}
