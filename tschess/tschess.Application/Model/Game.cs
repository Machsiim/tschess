using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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

        public Game(string player1, string player2)
        {
            Player1 = player1;
            Player2 = player2;
            GameState = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        
        // Change Players to GUID
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        
        public string? Winner { get; set; }

        public string GameState { get; set; } 

    }
}
