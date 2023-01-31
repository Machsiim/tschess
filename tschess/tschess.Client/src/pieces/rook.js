export class Rook extends Piece {
  constructor(currentRow, currentCol, color) {
    super(currentRow, currentCol, color);
  }
  isValidMove(destinationRow, destinationCol) {
    if (super.isValidMove(destinationRow, destinationCol)) {
      // Move along a row
      if (destinationRow === this.currentRow) {
        let start = Math.min(destinationCol, this.currentCol) + 1;
        let end = Math.max(destinationCol, this.currentCol);
        for (let col = start; col < end; col++) {
          if (this.board[destinationRow][col] !== null) {
            return false;
          }
        }
      }
      // Move along a column
      else if (destinationCol === this.currentCol) {
        let start = Math.min(destinationRow, this.currentRow) + 1;
        let end = Math.max(destinationRow, this.currentRow);
        for (let row = start; row < end; row++) {
          if (this.board[row][destinationCol] !== null) {
            return false;
          }
        }
      } else {
        return false;
      }

      if (
        this.board[destinationRow][destinationCol] === null ||
        this.board[destinationRow][destinationCol].color !== this.color
      ) {
        return true;
      }
    }

    return false;
  }
}
