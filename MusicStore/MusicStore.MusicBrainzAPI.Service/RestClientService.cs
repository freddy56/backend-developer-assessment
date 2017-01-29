using MusicStore.MusicBrainzAPI.IService;
using MusicStore.MusicBrainzAPI.Model;
using RestSharp;

namespace MusicStore.MusicBrainzAPI.Service
{
    public class RestClientService : IRestClientService
    {
        private RestClient _Client;

        public RestClientService(string baseUrl)
        {
            _Client = new RestClient(baseUrl);
        }

        public Releases GetArtistReleases(string id)
        {
            //http://musicbrainz.org/ws/2/release?artist=b83bc61f-8451-4a5d-8b8e-7e9ed295e822&inc=labels+media+artist-credits+&fmt=json
            var request = new RestRequest("release?artist={id}&inc=labels+media+artist-credits+&fmt=json", Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "application/json");
            var response = _Client.Execute<Releases>(request);

            if (response.Data != null)
                return response.Data;
            else
                return new Releases();
        }

        public Releases GetArtistAlbums(int offset, int numberOfAlbulms, string id)
        {
            //http://musicbrainz.org/ws/2/release?artist=b83bc61f-8451-4a5d-8b8e-7e9ed295e822&type=album&offset=0&limit=10&inc=release-groups+labels+media+artist-credits+&fmt=json
            var request = new RestRequest("release?artist={id}&type=album&offset={offset}&limit={numberOfAlbulms}&inc=release-groups+labels+media+artist-credits+&fmt=json", Method.GET);
            request.AddUrlSegment("id", id);
            request.AddUrlSegment("offset", offset.ToString());
            request.AddUrlSegment("numberOfAlbulms", numberOfAlbulms.ToString());
            request.AddHeader("Content-Type", "application/json");
            var response = _Client.Execute<Releases>(request);

            if (response.Data != null)
                return response.Data;
            else
                return new Releases();
        }
    }
}
