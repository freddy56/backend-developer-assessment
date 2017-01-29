
using AutoMapper;
using MusicStore.Data;
using MusicStore.Model;
using MusicStore.MusicBrainzAPI.Model;
using System;
using System.Collections.Generic;

namespace MusicStore.Handler

{
    public class BootStrapper
    {
        public static void InitializeMaps()
        {
            Mapper.CreateMap<MusicStore.Data.Artist, ArtistModel>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(c => c.ArtistName))
                .ForMember(dest => dest.country, opt => opt.MapFrom(c => c.Country))
                .AfterMap((artist, artistModel) =>
                {
                    if (!string.IsNullOrEmpty(artist.Aliases))
                    {
                        string[] stringSeparators = new string[] { "," };
                        artistModel.alias = artist.Aliases.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    }
                });

            Mapper.CreateMap<Releases, ArtistReleasesModel>();

            Mapper.CreateMap<Release, ArtistReleaseModel>()
                .ForMember(dest => dest.releaseId, opt => opt.MapFrom(c => c.id))
                .ForMember(dest => dest.otherArtists, opt => opt.Ignore())
                .AfterMap((release, artistReleaseModel) =>
                {
                    //populate lable
                    int count = 0;
                    foreach (var item in release.labelInfo)
                    {
                        if (count > 0)
                            artistReleaseModel.label = string.Format("{0},{1}", artistReleaseModel.label, item.label.name);
                        else
                            artistReleaseModel.label = item.label.name;
                        count++;
                    }

                    //populate number of tracks
                    count = 0;
                    foreach (var item in release.media)
                    {
                        if (count > 0)
                            artistReleaseModel.numberOfTracks += item.numberOfTracks;
                        else
                            artistReleaseModel.numberOfTracks = item.numberOfTracks;
                        count++;
                    }

                    //populate other artists
                    artistReleaseModel.otherArtists = new List<OtherArtistsModel>();
                    foreach (var item in release.otherArtists)
                    {
                        artistReleaseModel.otherArtists.Add(new OtherArtistsModel()
                        {
                            id = item.artist.id,
                            name = item.name
                        });
                    }
                });
        }
    }
}
