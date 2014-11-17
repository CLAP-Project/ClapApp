using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public class Localizacao
    {
        public int Id
        {
            get;
            set;
        }

        public int AnimalId
        {
            get;
            set;
        }

        public GeoCoordinate Coordenada
        {
            get;
            set;
        }

        public DateTime DataHora
        {
            get;
            set;
        }

        public string AltLong
        {
            get
            {
                return Coordenada.Latitude.ToString() + ", " + Coordenada.Longitude.ToString();
            }
        }

        public bool IsFotoQRCode
        {
            get;
            set;
        }

        // public Image FotoQRCode?

        // Apenas usada se for uma localização de QRCode.
        public string Descricao
        {
            get;
            set;
        }

        public Localizacao()
        {
            IsFotoQRCode = false;
            DataHora = new DateTime();
            Descricao = "";
        }

        private static GeoCoordinate cloneCoordenada(GeoCoordinate that)
        {
            return new GeoCoordinate()
            {
                Altitude = that.Altitude,
                Course = that.Course,
                HorizontalAccuracy = that.HorizontalAccuracy,
                Latitude = that.Latitude,
                Longitude = that.Longitude,
                Speed = that.Speed,
                VerticalAccuracy = that.VerticalAccuracy
            };
        }

        public Localizacao Assimilate(Localizacao that)
        {
            this.Id = that.Id;
            this.AnimalId = that.AnimalId;
            this.DataHora = that.DataHora;
            this.Coordenada = cloneCoordenada(that.Coordenada);
            this.IsFotoQRCode = that.IsFotoQRCode;
            this.Descricao = that.Descricao.Substring(0);

            return this;
        }

        public Localizacao Copy()
        {
            return new Localizacao().Assimilate(this);
        }
    }
}
