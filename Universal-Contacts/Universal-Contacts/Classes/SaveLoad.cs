using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Net.Http;

namespace Universal_Contacts
{
    class SaveLoad
    {
        public static async Task<bool> writeObjektAsync<T>(string file, T objekt)
        {
            try
            {
                StorageFile userdetailsfile = await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);

                IRandomAccessStream rndStream = await
                    userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);

                using (IOutputStream outStream = rndStream.GetOutputStreamAt(0))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof (T));
                    serializer.WriteObject(outStream.AsStreamForWrite(), objekt);
                    var xx = await outStream.FlushAsync();
                    rndStream.Dispose();
                    outStream.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }   
        }

        public static async Task<T> readObjektAsync<T>(string datei)
        {
            StorageFile file;
            IRandomAccessStream inStream = null;

            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(datei);
                inStream = await file.OpenAsync(FileAccessMode.Read);
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                var data = (T) serializer.ReadObject(inStream.AsStreamForRead());
                inStream.Dispose();

                return data;

            }
            catch (Exception)
            {
                if (inStream != null)
                    inStream.Dispose();
                return default(T);
            }
        }

        public static async Task<T> readXmlFile<T>(string datei)
        {
            try
            {
            var file = await
                            Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Data\" + datei + ".xml");
                        var stream = await file.OpenReadAsync();
                        var rdr = new StreamReader(stream.AsStream());
                        var contents = await rdr.ReadToEndAsync();

                        XmlReader reader = XmlReader.Create(new StringReader(contents));
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        var daten = (T) serializer.Deserialize(reader);
                        reader.Dispose();
                        return daten;
            }
            catch (Exception)
            {
                //create empy List
                throw;
            }  
        }

        //url für Testfile:
        //@"http://ainf.hiai.de/studium/spielwiese/myxmlfile.xml";
        public static async Task<T> readXmlFromHttp<T> (string url)
        {
            HttpWebRequest myHttpRequest = (HttpWebRequest) WebRequest.Create(url);
            myHttpRequest.Method = "GET"; //POST
            myHttpRequest.ContentType = "text/xml"; //etc

            try
            {
                var response = (HttpWebResponse)await myHttpRequest.GetResponseAsync();
                Stream resStream = response.GetResponseStream();

                string result;
                using (StreamReader read = new StreamReader(resStream))
                {
                    result = await read.ReadToEndAsync();
                    read.Dispose();
                }

                XmlReader reader = XmlReader.Create(new StringReader(result));
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var daten = (T) serializer.Deserialize(reader);
                reader.Dispose();
                return daten;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        //url für Testfile:
        //@"http://ainf.hiai.de/studium/spielwiese/Samer.php";
        public static async void writeXmlToHttp ()
        {
            using (var client = new HttpClient())
            {

                var values = new Dictionary<string, string>
                {
                    { "test", "test2" },
                    { "test3", "test4" }
                };

                var content = new FormUrlEncodedContent(values);


                var response = await client.PostAsync("http://ainf.hiai.de/studium/Samer.php", content);
                //oder : "SebastianPHPSkript.php"

                var responseString = await response.Content.ReadAsStringAsync();
            }

            //YASS LÖSUNG:
            //dat == List<Person> --> wird übergeben

            //var dat = "test";

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    XmlSerializer serializeMemory = new XmlSerializer(typeof(List<Person>));
            //    serializeMemory.Serialize(stream, dat);
            //    StreamReader streamR = new StreamReader(stream);
            //    stream.Position = 0;
            //    var upDaten = streamR.ReadToEnd();

            //    Uri requestUri = new Uri(@"http://ainf.hiai.de/studium/Samer2.php");
            //    var objClint = new HttpClient();
            //    HttpResponseMessage response =
            //        await
            //            objClint.PostAsync(requestUri,
            //                new StringContent(upDaten, System.Text.Encoding.UTF8, "application/xml"));
            //}
        }
    }
}
