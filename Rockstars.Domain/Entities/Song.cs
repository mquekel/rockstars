namespace Rockstars.Domain.Entities
{
    public class Song
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string Artist { get; set; }

        public string Shortname { get; set; }

        public long Bpm { get; set; }

        public long Duration { get; set; }

        public string Genre { get; set; }

        public string SpotifyId { get; set; }

        public string Album { get; set; }
    }
}
