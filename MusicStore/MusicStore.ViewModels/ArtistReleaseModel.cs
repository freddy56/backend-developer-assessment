using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Model
{
    public class ArtistReleaseModel
    {
        public string releaseId { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string label {get; set;}
        public string numberOfTracks { get; set; }
        public List<OtherArtistsModel> otherArtists { get; set; }
    }
}
