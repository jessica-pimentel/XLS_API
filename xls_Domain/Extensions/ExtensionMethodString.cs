using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace xls_Domain.Extensions
{
    public static class ExtensionMethodString
    {
        public static List<int> SplitToIntList(this string list, char separator = ',')
        {
            return list.Split(separator).Select(Int32.Parse).OrderBy(ew => ew).ToList();
        }

        public static string ToUpperCase(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value.ToUpper();
        }

        public static string ToLowerCase(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value.ToLower();
        }

        public static string RemoveAccents(this string value, int? maxRetun = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            var s = Regex.Replace(value.Normalize(NormalizationForm.FormD), @"[^A-Za-z 0-9 \.]*", "").Trim();

            if (maxRetun.HasValue)
            {
                s = s.Length > maxRetun ? s.Substring(0, maxRetun.Value) : s;
            }

            return s;

            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < value.Length; i++)
            //    sb.Append(s_Accents[value[i]]);

            //return sb.ToString();
        }

        public static string MaxString(this string value, int maxRetun)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            var s = value.Length > maxRetun ? value.Substring(0, maxRetun) : value;

            return s;
        }

        private static readonly char[] s_Accents = GetAccents();

        private static char[] GetAccents()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'\''] = ' ';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';

            accents[(byte)'&'] = ' ';

            return accents;
        }

        public static LinkedResource GenerateCIDImage(this StringBuilder value, string directoryOut, string nameImage, string extension)
        {
            var cid = Guid.NewGuid().ToString();

            var imag = $"{directoryOut}\\Mail\\Templates\\Images\\{nameImage}.{extension}";

            var l = new LinkedResource(imag, "image/png");

            l.ContentId = cid;

            return l;
        }

        public static LinkedResource GenerateCIDImage(this StringBuilder value, string fullPath)
        {
            var cid = Guid.NewGuid().ToString();

            var l = new LinkedResource(fullPath);

            l.ContentId = cid;

            return l;
        }

        //public static string FormatCellAndPhoneNumner(this string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        return "(00) 0 0000-0000";
        //    }

        //    value = value.Length < 8 ? OnlyNumbers(value).PadLeft(8, '0') : OnlyNumbers(value);

        //    string number = string.Empty;

        //    if (value.Length == 8) //without ddd
        //    {
        //        number = string.Format(@"{0:\(xx\) 0000\-0000}", value.StringToInt64());
        //    }

        //    if (value.Length == 9)
        //    {
        //        number = string.Format(@"{0:(xx) 0 0000\-0000}", value.StringToInt64());
        //    }

        //    if (value.Length == 10) //without ddd
        //    {
        //        number = string.Format(@"{0:\(00\) 0000\-0000}", value.StringToInt64());
        //    }

        //    if (value.Length > 10)
        //    {
        //        number = string.Format(@"{0:\(00\) 0 0000\-0000}", value.StringToInt64());
        //    }

        //    return number;
        //}

        //public static string FormatCellAndPhoneNumnerLPGD(this string value)
        //{
        //    value = value.FormatCellAndPhoneNumner();

        //    var s = value.Substring(value.Length - 4, 4);

        //    return s;
        //}

        public static string FormatPostalCode(this string value, bool sqlRegister = false)
        {
            value = string.IsNullOrEmpty(value) ? "0" : value;

            var s = sqlRegister == true ? OnlyNumbers(value).PadLeft(8, '0') : Convert.ToInt64(OnlyNumbers(value)).ToString("#####-###");

            return s;
        }

        //public static string FormatDocument(this string value, bool sqlRegister = false, EClientType type = EClientType.NaturalPerson)
        //{
        //    value = string.IsNullOrEmpty(value) ? "0" : value;

        //    if (sqlRegister)
        //    {
        //        return type == EClientType.NaturalPerson ? OnlyNumbers(value).PadLeft(11, '0') : OnlyNumbers(value).PadLeft(14, '0');
        //    }

        //    if (value.Length <= 11)
        //    {
        //        return string.Format(@"{0:000\.000\.000\-00}", value.OnlyNumbers().StringToInt64());
        //    }
        //    else
        //    {
        //        return string.Format(@"{0:00\.000\.000\/0000\-00}", value.OnlyNumbers().StringToInt64());
        //    }
        //}

        public static string OnlyNumbers(this string value, bool acceptNull = false)
        {
            if (acceptNull && string.IsNullOrEmpty(value))
            {
                return null;
            }

            value = string.IsNullOrEmpty(value) ? "0" : value;

            Regex regexObj = new Regex(@"[^\d]");

            return regexObj.Replace(value, "");
        }

        public static string OnlyNumbersAndDot(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "0" : value;

            Regex regexObj = new Regex(@"[^0-9.]");

            return regexObj.Replace(value, "");
        }

        public static bool IsEmailValid(this string value)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return rg.IsMatch(value);
        }

        public static bool IsCellPhoneValid(this string value)
        {
            Regex rg = new Regex(@"^\s*[1-9](\d{2}|\d{0})[-. ]?(\d{5}|\d{4})[-. ]?(\d{4})[-. ]?\s*$");

            return rg.IsMatch(value);
        }

        public static String FormatDateFiscal(this DateTime data)
        {
            return data.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        public static Double ToDouble(this string value)
        {
            Double v;
            Double.TryParse(value, out v);
            return v;
        }

        public static string Capitalize(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string ToUTF8(this string value)
        {
            byte[] bytes = Encoding.Default.GetBytes(value);

            return Encoding.UTF8.GetString(bytes);
        }

        public static string FormatOurNumber(this string value, int quantity)
        {
            try
            {
                return value.OnlyNumbers().ToString().PadRight(quantity, '0');
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public static string FormatFriendlyCreditCardRetun(this string value)
        {
            if (value.Contains("Card Expired")) return "Cartão expirado";
            if (value.Contains("Not Authorized")) return "Cartão não autorizado";
            if (value.Contains("Blocked Card")) return "Cartão bloqueado";
            if (value.Contains("Card Canceled")) return "Cartão cancelado";
            if (value.Contains("Problems with Creditcard")) return "Problemas com seu cartão";

            return value;
        }

        public static string FormatFriendlyId(this Guid value)
        {
            return value.ToString().Substring(0, 4).ToUpper();
        }

        public static string FormatFriendlyId(this Guid? value)
        {
            return value.HasValue ? value.ToString().Substring(0, 4).ToUpper() : "";
        }

        public static String FormatOurNumberLeft(this String nosso_Number, int qtde)
        {
            try
            {
                return nosso_Number.OnlyNumbers().ToString().PadLeft(qtde, '0');
            }
            catch (Exception)
            {
                return nosso_Number.ToString();
            }
        }

        //public static string FormatMaxLength(this string value, int length, bool removeEspecialCharacter = t)
        //{
        //    var rs = value.Length > length ? value.Substring(0, length) : value;

        //    return rs.Replace(System.Environment.NewLine, "");
        //}

        public static bool FormatToBoolean(this string value)
        {
            return string.IsNullOrEmpty(value) ? false : value.ToLower() == "sim" ? true : false;
        }

        public static string KeyAccessDetail(this string value)
        {
            string key = "";

            if (!string.IsNullOrEmpty(value))
            {
                value = value.OnlyNumbers().Replace(" ", "");

                for (int i = 0; i < 44; i += 4)
                {
                    key += $"{value.Substring(i, 4)} "; //dar espaco a casa 4 posicoes
                }
            }

            return key;
        }

        public static bool DocumentIsValid(this string value)
        {
            if (value.Length > 11)
            {
                return CNPJIsValid(value);
            }
            else
            {
                return CPFIsValid(value);
            }
        }

        public static bool CNPJIsValid(this string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            //not validate if null
            if (string.IsNullOrEmpty(value))
            {
                return true;
            };

            if (invalidNumbers.Contains(value))
            {
                return false;
            };

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Length != 14)
                return false;
            tempCnpj = value.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return value.EndsWith(digito);
        }

        public static bool CPFIsValid(this string value)
        {
            try
            {
                string[] invalidNumbers =
                {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

                //not validate if null
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                };

                if (invalidNumbers.Contains(value))
                {
                    return false;
                };

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempvalue;
                string digito;
                int soma;
                int resto;
                value = value.Trim();
                value = value.Replace(".", "").Replace("-", "");
                if (value.Length != 11)
                    return false;
                tempvalue = value.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempvalue[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempvalue = tempvalue + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempvalue[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return value.EndsWith(digito);
            }
            catch (Exception ex)
            {
                return false; //crashe
            }
        }

        public static bool IsEmpty(this string value)
        {
            if (value == null)
                return true;

            return string.IsNullOrEmpty(value);
        }

        public static string IsNull(this string value, string returnWith = "")
        {
            return value == null ? returnWith : value;
        }

        public static string FormatToFriendlyName(this bool value)
        {
            return value ? "Sim" : "Não";
        }

        public static string GetStateInitials(this int? value)
        {
            if (!value.HasValue)
            {
                return "";
            }

            /*
                11	RONDONIA	RO
                12	ACRE	AC
                13	AMAZONAS	AM
                14	RORAIMA	RR
                15	PARA	PA
                16	AMAPA	AP
                17	TOCANTINS	TO
                21	MARANHAO	MA
                22	PIAUI	PI
                23	CEARA	CE
                24	RIO GRANDE DO NORTE	RN
                25	PARAIBA	PB
                26	PERNAMBUCO	PE
                27	ALAGOAS	AL
                28	SERGIPE	SE
                29	BAHIA	BA
                31	MINAS GERAIS	MG
                32	ESPIRITO SANTO	ES
                33	RIO DE JANEIRO	RJ
                35	SAO PAULO	SP
                41	PARANA	PR
                42	SANTA CATARINA	SC
                43	RIO GRANDE DO SUL	RS
                50	MATO GROSSO DO SUL	MS
                51	MATO GROSSO	MT
                52	GOIAS	GO
                53	DISTRITO FEDERAL	DF

             */

            switch (value)
            {
                case 11: return "RO";
                case 12: return "AC";
                case 13: return "AM";
                case 14: return "RR";
                case 15: return "PA";
                case 16: return "AP";
                case 17: return "TO";
                case 21: return "MA";
                case 22: return "PI";
                case 23: return "CE";
                case 24: return "RN";
                case 25: return "PB";
                case 26: return "PE";
                case 27: return "AL";
                case 28: return "SE";
                case 29: return "BA";
                case 31: return "MG";
                case 32: return "ES";
                case 33: return "RJ";
                case 35: return "SP";
                case 41: return "PR";
                case 42: return "SC";
                case 43: return "RS";
                case 50: return "MS";
                case 51: return "MT";
                case 52: return "GO";
                case 53: return "DF";
            }

            return "";
        }

        public static string JsonSerialize(this object value)
        {
            var rs = JsonConvert.SerializeObject(value, Formatting.None,
                          new JsonSerializerSettings
                          {
                              NullValueHandling = NullValueHandling.Ignore
                          });

            return rs.Replace("'", "").Replace("\n", "");
        }

    }
}
