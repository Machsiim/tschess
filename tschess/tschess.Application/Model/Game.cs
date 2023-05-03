using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tschess.Application.Model
{
    public class Game
    {
        #pragma warning disable CS8618
        protected Game() { }
        #pragma warning restore CS8618

        public Game(Guid player1, Guid player2)
        {
            Player1 = player1;
            Player2 = player2; 
        }

        public Guid Guid { get; set; }
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }
        public Guid? Winner { get; set; }

        public char[][] GameState { get; set; } 

    }
}
