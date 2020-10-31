using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BakalarPrace.Models
{
    public class CsvRow
    {
        [Name("Datum")]
        public string Datum { get; set; }
        [Name("Cas")]   
        public string Cas { get; set; }
        [Name("Vysledek")]
        public string Vysledek { get; set; }
        [Name("Mod")]
        public string Mod { get; set; }
        [Name("Nejlepsi shoda")]
        public string Nejlepsi_shoda { get; set; }
        [Name("Nejlepsi shoda MN")]
        public string Nejlepsi_shoda_MN { get; set; }
        [Name("Uplynuly cas 1")]
        public string Uplynuly_cas_1 { get; set; }
        [Name("Uplynuly cas 2")]
        public string Uplynuly_cas_2 { get; set; }
        [Name("Cas celkem")]
        public string Cas_celkem { get; set; }
        [Name("Nazev Pole 1")]
        public string Pole1 { get; set; }
        [Name("Pole 1")]
        public string Pole1_Hodnota { get; set; }
        [Name("Nazev Pole 2")]
        public string Pole2 { get; set; }
        [Name("Pole 2")]
        public string Pole2_Hodnota { get; set; }
        [Name("Nazev Pole 3")]
        public string Pole3 { get; set; }
        [Name("Pole 3")]
        public string Pole3_Hodnota { get; set; }
        [Name("Nazev Pole 4")]
        public string Pole4 { get; set; }
        [Name("Pole 4")]
        public string Pole4_Hodnota { get; set; }
        [Name("Nazev Pole 5")]
        public string Pole5 { get; set; }
        [Name("Pole 5")]
        public string Pole5_Hodnota { get; set; }
        [Name("Nazev Pole 6")]
        public string Pole6 { get; set; }
        [Name("Pole 6")]
        public string Pole6_Hodnota { get; set; }
        [Name("Nazev Pole 7")]
        public string Pole7 { get; set; }
        [Name("Pole 7")]
        public string Pole7_Hodnota { get; set; }
        [Name("Nazev Pole 8")]
        public string Pole8 { get; set; }
        [Name("Pole 8")]
        public string Pole8_Hodnota { get; set; }
        [Name("LOD Sigma")]
        public string LOD { get; set; }
        [Name("Mg")]
        public string Mg { get; set; }
        [Name("Mg +/-")]
        public string Mg_Advanced { get; set; }
        [Name("Al")]
        public string Al { get; set; }
        [Name("Al +/-")]
        public string Al_Advanced { get; set; }
        [Name("Si")]
        public string Si { get; set; }
        [Name("Si +/-")]
        public string Si_Advanced { get; set; }
        [Name("P")]
        public string P { get; set; }
        [Name("P +/-")]
        public string P_Advanced { get; set; }
        [Name("S")]
        public string S { get; set; }
        [Name("S +/-")]
        public string S_Advanced { get; set; }
        [Name("Cl")]
        public string Cl { get; set; }
        [Name("Cl +/-")]
        public string Cl_Advanced { get; set; }
        [Name("K")]
        public string K { get; set; }
        [Name("K +/-")]
        public string K_Advanced { get; set; }
        [Name("Ca")]
        public string Ca { get; set; }
        [Name("Ca +/-")]
        public string Ca_Advanced { get; set; }
        [Name("Ti")]
        public string Ti { get; set; }
        [Name("Ti +/-")]
        public string Ti_Advanced { get; set; }
        [Name("Cr")]
        public string Cr { get; set; }
        [Name("Cr +/-")]
        public string Cr_Advanced { get; set; }
        [Name("Mn")]
        public string Mn { get; set; }
        [Name("Mn +/-")]
        public string Mn_Advanced { get; set; }
        [Name("Fe")]
        public string Fe { get; set; }
        [Name("Fe +/-")]
        public string Fe_Advanced { get; set; }
        [Name("Co")]
        public string Co { get; set; }
        [Name("Co +/-")]
        public string Co_Advanced { get; set; }
        [Name("Ni")]
        public string Ni { get; set; }
        [Name("Ni +/-")]
        public string Ni_Advanced { get; set; } 
        [Name("Cu")]
        public string Cu { get; set; }
        [Name("Cu +/-")]
        public string Cu_Advanced { get; set; }
        [Name("Zn")]
        public string Zn { get; set; }
        [Name("Zn +/-")]
        public string Zn_Advanced { get; set; }
        [Name("As")]
        public string As { get; set; }
        [Name("As +/-")]
        public string As_Advanced { get; set; }
        [Name("Se")]
        public string Se { get; set; }
        [Name("Se +/-")]
        public string Se_Advanced { get; set; }
        [Name("Rb")]
        public string Rb { get; set; }
        [Name("Rb +/-")]
        public string Rb_Advanced { get; set; }
        [Name("Sr")]
        public string Sr { get; set; }
        [Name("Sr +/-")]
        public string Sr_Advanced { get; set; }
        [Name("Y")]
        public string Y { get; set; }
        [Name("Y +/-")]
        public string Y_Advanced { get; set; }
        [Name("Zr")]
        public string Zr { get; set; }
        [Name("Zr +/-")]
        public string Zr_Advanced { get; set; }
        [Name("Mo")]
        public string Mo { get; set; }
        [Name("Mo +/-")]
        public string Mo_Advanced { get; set; }
        [Name("Ag")]
        public string Ag { get; set; }
        [Name("Ag +/-")]
        public string Ag_Advanced { get; set; }
        [Name("Cd")]
        public string Cd { get; set; }
        [Name("Cd +/-")]
        public string Cd_Advanced { get; set; }
        [Name("In ")]
        public string In { get; set; }
        [Name("In +/-")]
        public string In_Advanced { get; set; }
        [Name("Sn")]
        public string Sn { get; set; }
        [Name("Sn +/-")]
        public string Sn_Advanced { get; set; }
        [Name("Sb ")]
        public string Sb { get; set; }
        [Name("Sb +/-")]
        public string Sb_Advanced { get; set; }
        [Name("Ba ")]
        public string Ba { get; set; }
        [Name("Ba +/-")]
        public string Ba_Advanced { get; set; }
        [Name("W")]
        public string W { get; set; }
        [Name("W +/-")]
        public string W_Advanced { get; set; }
        [Name("Au")]
        public string Au { get; set; }
        [Name("Au +/-")]
        public string Au_Advanced { get; set; }
        [Name("Hg")]
        public string Hg { get; set; }
        [Name("Hg +/-")]
        public string Hg_Advanced { get; set; }
        [Name("Tl")]
        public string Tl { get; set; }
        [Name("Tl +/-")]
        public string Tl_Advanced { get; set; }
        [Name("Pb")]
        public string Pb { get; set; }
        [Name("Pb +/-")]
        public string Pb_Advanced { get; set; }
        [Name("Bi")]
        public string Bi { get; set; }
        [Name("Bi +/-")]
        public string Bi_Advanced { get; set; }
        [Name("Th ")]
        public string Th { get; set; }
        [Name("Th +/-")]
        public string Th_Advanced { get; set; }
        [Name("U")]
        public string U { get; set; }
        [Name("U +/-")]
        public string U_Advanced { get; set; }
        [Name("LE")]
        public string LE { get; set; }
        [Name("LE +/-")]
        public string LE_Advanced { get; set; }
        [Name("2. nejlepsi shoda")]
        public string Nejlepsi_shoda2 { get; set; }
        [Name("2. nejlepsi shoda MN")]
        public string Nejlepsi_shoda2_MN { get; set; }
        [Name("3. nejlepsi shoda")]
        public string Nejlepsi_shoda3 { get; set; }
        [Name("3. nejlepsi shoda MN")]
        public string Nejlepsi_shoda3_MN { get; set; }
        [Name("Nazev Pole 9")]
        public string Pole9 { get; set; }
        [Name("Pole 9")]
        public string Pole9_Hodnota { get; set; }
        [Name("Nazev Pole 10")]
        public string Pole10 { get; set; }
        [Name("Pole 10")]
        public string Pole10_Hodnota { get; set; }
        [Name("Nazev Pole 11")]
        public string Pole11 { get; set; }
        [Name("Pole 11")]
        public string Pole11_Hodnota { get; set; }
        [Name("Nazev Pole 12")]
        public string Pole12 { get; set; }
        [Name("Pole 12")]
        public string Pole12_Hodnota { get; set; }
        [Name("Nazev Pole 13")]
        public string Pole13 { get; set; }
        [Name("Pole 13")]
        public string Pole13_Hodnota { get; set; }
        [Name("Nazev Pole 14")]
        public string Pole14 { get; set; }
        [Name("Pole 14")]
        public string Pole14_Hodnota { get; set; }
        [Name("Nazev Pole 15")]
        public string Pole15 { get; set; }
        [Name("Pole 15")]
        public string Pole15_Hodnota { get; set; }
        [Name("Nazev Pole 16")]
        public string Pole16 { get; set; }
        [Name("Pole 16")]
        public string Pole16_Hodnota { get; set; }

    }
}
