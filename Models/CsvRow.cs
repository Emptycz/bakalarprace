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

        //string datum, string cas,
        public CsvRow(int ID, int RowNumber, string datum, string cas, string vysledek, string mod, string nejlepsi_shoda, string nejlepsi_shoda_MN, string nejlepsi_shoda2, string nejlepsi_shoda2_MN, string nejlepsi_shoda3, string nejlepsi_shoda3_MN, string uplynuly_cas_1, string uplynuly_cas_2, string cas_celkem, string lOD, string pole1, string pole1_Hodnota, string pole2, string pole2_Hodnota, string pole3, string pole3_Hodnota, string pole4, string pole4_Hodnota, string pole5, string pole5_Hodnota, string pole6, string pole6_Hodnota, string pole7, string pole7_Hodnota, string pole8, string pole8_Hodnota, string pole9, string pole9_Hodnota, string pole10, string pole10_Hodnota, string pole11, string pole11_Hodnota, string pole12, string pole12_Hodnota, string pole13, string pole13_Hodnota, string pole14, string pole14_Hodnota, string pole15, string pole15_Hodnota, string pole16, string pole16_Hodnota, string mg, string mg_Advanced, string al, string al_Advanced, string si, string si_Advanced, string p, string p_Advanced, string s, string advanced, string cl, string cl_Advanced, string k, string k_Advanced, string ca, string ca_Advanced, string ti, string ti_Advanced, string cr, string cr_Advanced, string mn, string mn_Advanced, string fe, string fe_Advanced, string co, string co_Advanced, string ni, string ni_Advanced, string cu, string cu_Advanced, string zn, string zn_Advanced, string @as, string as_Advanced, string se, string se_Advanced, string rb, string rb_Advanced, string sr, string sr_Advanced, string y, string y_Advanced, string zr, string zr_Advanced, string mo, string mo_Advanced, string ag, string ag_Advanced, string cd, string cd_Advanced, string @in, string in_Advanced, string sn, string sn_Advanced, string sb, string sb_Advanced, string ba, string ba_Advanced, string w, string w_Advanced, string au, string au_Advanced, string hg, string hg_Advanced, string tl, string tl_Advanced, string pb, string pb_Advanced, string bi, string bi_Advanced, string th, string th_Advanced, string u, string u_Advanced, string lE, string lE_Advanced)
        {
            Datum = datum;
            Cas = cas;
            Vysledek = vysledek;
            Mod = mod;
            Nejlepsi_shoda = nejlepsi_shoda;
            Nejlepsi_shoda_MN = nejlepsi_shoda_MN;
            Uplynuly_cas_1 = uplynuly_cas_1;
            Uplynuly_cas_2 = uplynuly_cas_2;
            Cas_celkem = cas_celkem;
            Pole1 = pole1;
            Pole1_Hodnota = pole1_Hodnota;
            Pole2 = pole2;
            Pole2_Hodnota = pole2_Hodnota;
            Pole3 = pole3;
            Pole3_Hodnota = pole3_Hodnota;
            Pole4 = pole4;
            Pole4_Hodnota = pole4_Hodnota;
            Pole5 = pole5;
            Pole5_Hodnota = pole5_Hodnota;
            Pole6 = pole6;
            Pole6_Hodnota = pole6_Hodnota;
            Pole7 = pole7;
            Pole7_Hodnota = pole7_Hodnota;
            Pole8 = pole8;
            Pole8_Hodnota = pole8_Hodnota;
            LOD = lOD;
            Mg = mg;
            Mg_Advanced = mg_Advanced;
            Al = al;
            Al_Advanced = al_Advanced;
            Si = si;
            Si_Advanced = si_Advanced;
            P = p;
            P_Advanced = p_Advanced;
            S = s;
            S_Advanced = advanced;
            Cl = cl;
            Cl_Advanced = cl_Advanced;
            K = k;
            K_Advanced = k_Advanced;
            Ca = ca;
            Ca_Advanced = ca_Advanced;
            Ti = ti;
            Ti_Advanced = ti_Advanced;
            Cr = cr;
            Cr_Advanced = cr_Advanced;
            Mn = mn;
            Mn_Advanced = mn_Advanced;
            Fe = fe;
            Fe_Advanced = fe_Advanced;
            Co = co;
            Co_Advanced = co_Advanced;
            Ni = ni;
            Ni_Advanced = ni_Advanced;
            Cu = cu;
            Cu_Advanced = cu_Advanced;
            Zn = zn;
            Zn_Advanced = zn_Advanced;
            As = @as;
            As_Advanced = as_Advanced;
            Se = se;
            Se_Advanced = se_Advanced;
            Rb = rb;
            Rb_Advanced = rb_Advanced;
            Sr = sr;
            Sr_Advanced = sr_Advanced;
            Y = y;
            Y_Advanced = y_Advanced;
            Zr = zr;
            Zr_Advanced = zr_Advanced;
            Mo = mo;
            Mo_Advanced = mo_Advanced;
            Ag = ag;
            Ag_Advanced = ag_Advanced;
            Cd = cd;
            Cd_Advanced = cd_Advanced;
            In = @in;
            In_Advanced = in_Advanced;
            Sn = sn;
            Sn_Advanced = sn_Advanced;
            Sb = sb;
            Sb_Advanced = sb_Advanced;
            Ba = ba;
            Ba_Advanced = ba_Advanced;
            W = w;
            W_Advanced = w_Advanced;
            Au = au;
            Au_Advanced = au_Advanced;
            Hg = hg;
            Hg_Advanced = hg_Advanced;
            Tl = tl;
            Tl_Advanced = tl_Advanced;
            Pb = pb;
            Pb_Advanced = pb_Advanced;
            Bi = bi;
            Bi_Advanced = bi_Advanced;
            Th = th;
            Th_Advanced = th_Advanced;
            U = u;
            U_Advanced = u_Advanced;
            LE = lE;
            LE_Advanced = lE_Advanced;
            Nejlepsi_shoda2 = nejlepsi_shoda2;
            Nejlepsi_shoda2_MN = nejlepsi_shoda2_MN;
            Nejlepsi_shoda3 = nejlepsi_shoda3;
            Nejlepsi_shoda3_MN = nejlepsi_shoda3_MN;
            Pole9 = pole9;
            Pole9_Hodnota = pole9_Hodnota;
            Pole10 = pole10;
            Pole10_Hodnota = pole10_Hodnota;
            Pole11 = pole11;
            Pole11_Hodnota = pole11_Hodnota;
            Pole12 = pole12;
            Pole12_Hodnota = pole12_Hodnota;
            Pole13 = pole13;
            Pole13_Hodnota = pole13_Hodnota;
            Pole14 = pole14;
            Pole14_Hodnota = pole14_Hodnota;
            Pole15 = pole15;
            Pole15_Hodnota = pole15_Hodnota;
            Pole16 = pole16;
            Pole16_Hodnota = pole16_Hodnota;
        }
    }
}
