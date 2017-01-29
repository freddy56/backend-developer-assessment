using MusicStore.MusicBrainzAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.MusicBrainzAPI.IService
{
    public interface IRestClientService
    {
        Releases GetArtistReleases(string id);
        Releases GetArtistAlbums(int offset, int numberOfAlbulms, string id);
    }
}
