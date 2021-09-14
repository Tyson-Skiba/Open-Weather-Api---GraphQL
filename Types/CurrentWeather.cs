namespace WeatherApi
{
    public class CurrentWeather 
    {
        public int Id { get; set; }
        public double Timezone { get; set; }
        public string Name { get; set;}
        public double Cod { get; set; }
        public string Base { get; set; }
        public int Visibility { get; set; }
        public long Dt { get; set; }

        public Summary Main { get; set; }
        public Clouds Clouds { get; set; }
        public SystemValue Sys { get; set; }
        public Coord Coord { get; set; }
        public Wind Wind { get; set; }
        public Weather[] Weather { get; set; }
    }
}
