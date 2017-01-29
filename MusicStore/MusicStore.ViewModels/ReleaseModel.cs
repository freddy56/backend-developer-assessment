using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Model
{
    public class ReleaseModel
    {
        public string releaseId;
        public string title;
        public string status;
        public string label;
        public string numberOfTracks;
        public List<OtherArtistsModel> otherArtists;
    }
}
