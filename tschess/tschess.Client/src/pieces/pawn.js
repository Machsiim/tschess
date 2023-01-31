export class Pawn extends Piece {
  constructor(currentRow, currentCol, color) {
    super(currentRow, currentCol, color);
  }
  isValidMove(destinationRow, destinationCol) {
    if (super.isValidMove(destinationRow, destinationCol)) {
      let rowDiff = destinationRow - this.currentRow;
      let colDiff = Math.abs(destinationCol - this.currentCol);

      if (this.color === "white") {
        if (
          destinationCol === this.currentCol &&
          rowDiff === -1 &&
          this.board[destinationRow][destinationCol] === null
        ) {
          return true;
        } else if (
          colDiff === 1 &&
          rowDiff === -1 &&
          this.board[destinationRow][destinationCol] !== null
        ) {
          return true;
        } else if (
          this.currentRow === 6 &&
          rowDiff === -2 &&
          colDiff === 0 &&
          this.board[destinationRow][destinationCol] === null &&
          this.board[destinationRow + 1][destinationCol] === null
        ) {
          return true;
        }
      } else if (this.color === "black") {
        if (
          destinationCol === this.currentCol &&
          rowDiff === 1 &&
          this.board[destinationRow][destinationCol] === null
        ) {
          return true;
        } else if (
          colDiff === 1 &&
          rowDiff === 1 &&
          this.board[destinationRow][destinationCol] !== null
        ) {
          return true;
        } else if (
          this.currentRow === 1 &&
          rowDiff === 2 &&
          colDiff === 0 &&
          this.board[destinationRow][destinationCol] === null &&
          this.board[destinationRow - 1][destinationCol] === null
        ) {
          return true;
        }
      }
    }

    return false;
  }
}
