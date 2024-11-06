namespace ChamadaKarate.core.BLL
{
    public class Configuracao
    {
        public static string strConnection { get; set; }

        public static string getStrConnection(string ambiente)
        {
            if (ambiente == "PRD")
            {
                strConnection = "Host=localhost;Port=5432;Username=chamadakarate;Password=ChamadaKarate;Database=chamadakarate;";
            }
            else if(ambiente == "DEV")
            {
                strConnection = "Host=localhost;Port=5432;Username=chamadakarate;Password=ChamadaKarate;Database=chamadakarate;";
            }
            else
            {
                throw new Exception("Tipo de ambiente incorreto");
            }
            return strConnection;
        }
    }
}
