// import all pieces from the pieces folder
import { Piece, King, Queen, Bishop, Knight, Rook, Pawn } from "./pieces";
// initialize the chess board and set all the pieces to their starting positions
export class Game {
  constructor() {
    this.currentPlayer = "white";
    this.gameOver = false;
    this.isInCheck = false;
    this.board = new Array(8);
    for (let i = 0; i < this.board.length; i++) {
      this.board[i] = new Array(8);
    }
    this.board[0][0] = new Rook("black");
    this.board[0][1] = new Knight("black");
    this.board[0][2] = new Bishop("black");
    this.board[0][3] = new Queen("black");
    this.board[0][4] = new King("black");
    this.board[0][5] = new Bishop("black");
    this.board[0][6] = new Knight("black");
    this.board[0][7] = new Rook("black");
    for (let i = 0; i < 8; i++) {
      this.board[1][i] = new Pawn("black");
    }
    this.board[7][0] = new Rook("white");
    this.board[7][1] = new Knight("white");
    this.board[7][2] = new Bishop("white");
    this.board[7][3] = new Queen("white");
    this.board[7][4] = new King("white");
    this.board[7][5] = new Bishop("white");
    this.board[7][6] = new Knight("white");
    this.board[7][7] = new Rook("white");
    for (let i = 0; i < 8; i++) {
      this.board[6][i] = new Pawn("white");
    }
    const _ = require("lodash");
    board2 = _.cloneDeep(board);
  }
  // check if the move is valid
  isValidMove(piece, x, y) {
    if (piece.isValidMove(x, y)) {
      return true;
    } else {
      return false;
    }
  }
  // check if the move is legal
  isLegalMove(piece, x, y) {
    if (piece.isLegalMove(x, y)) {
      return true;
    } else {
      return false;
    }
  }
  // check if the move is legal and valid
  isLegalAndValidMove(piece, x, y) {
    if (this.isValidMove(piece, x, y) && this.isLegalMove(piece, x, y)) {
      return true;
    } else {
      return false;
    }
  }
  // check for check
  checkKingInCheck(board) {
    let king;
    for (let i = 0; i < board.length; i++) {
      for (let j = 0; j < board.length; j++) {
        if (
          board[i][j] instanceof King &&
          board[i][j].color === this.currentPlayer
        ) {
          king = board[i][j];
          break;
        }
      }
    }
    for (let i = 0; i < board.length; i++) {
      for (let j = 0; j < board.length; j++) {
        if (board[i][j] && board[i][j].color !== this.currentPlayer) {
          if (board[i][j].isLegalMove(king.currentRow, king.currentCol)) {
            isInCheck = true;
            return true;
          }
        }
      }
    }
  }

  // check for checkmate
  checkForCheckmate() {
    let king;
    for (let i = 0; i < board.length; i++) {
      for (let j = 0; j < board.length; j++) {
        if (
          board[i][j] instanceof King &&
          board[i][j].color === this.currentPlayer
        ) {
          king = board[i][j];
          break;
        }
      }
    }
    let kingRow = king.currentRow;
    let kingCol = king.currentCol;
    let kingMoves = [
      [kingRow - 1, kingCol - 1],
      [kingRow - 1, kingCol],
      [kingRow - 1, kingCol + 1],
      [kingRow, kingCol - 1],
      [kingRow, kingCol + 1],
      [kingRow + 1, kingCol - 1],
      [kingRow + 1, kingCol],
      [kingRow + 1, kingCol + 1],
    ];
    for (let i = 0; i < kingMoves.length; i++) {
      if (this.isLegalAndValidMove(king, kingMoves[i][0], kingMoves[i][1])) {
        return false;
      }
    }
    for (let i = 0; i < board.length; i++) {
      for (let j = 0; j < board.length; j++) {
        if (board[i][j] && board[i][j].color === this.currentPlayer) {
          let piece = board[i][j];
          for (let k = 0; k < board.length; k++) {
            for (let l = 0; l < board.length; l++) {
              if (this.isLegalAndValidMove(piece, k, l)) {
                return false;
              }
            }
          }
        }
      }
    }
    return true;
  }

  makeMove(piece, x, y) {
    if (piece.color !== this.currentPlayer) return false;
    if (this.isLegalAndValidMove(piece, x, y)) {
      board2[x][y] = piece;
      board2[piece.currentRow][piece.currentCol] = null;
      if (this.checkKingInCheck(board2)) {
        baord2 = _.cloneDeep(board);
        return false;
      } else board = _.cloneDeep(board2);

      this.currentPlayer = this.currentPlayer === "white" ? "black" : "white";
      piece.currentCol = y;
      piece.currentRow = x;
      return true;
    } else return false;
  }
}
