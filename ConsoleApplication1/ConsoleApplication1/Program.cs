using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://deckofcardsapi.com/api/deck");
            var newDeckRrequest = new RestRequest("deck/new/shuffle");
            var response = client.Execute<Deck>(newDeckRrequest);


        }
    }

    internal class Deck
    {public string Deck_Id { get; set; }
        public int Remaining { get; set; }
        public bool Shuffled { get; set; }
        public bool Success { get; set; }
    }
}
