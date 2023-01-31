export class Queen extends Piece {
  constructor(currentRow, currentCol, color) {
    super(currentRow, currentCol, color);
  }
  isValidMove(destinationRow, destinationCol) {
    if (super.isValidMove(destinationRow, destinationCol)) {
      let rowDiff = Math.abs(destinationRow - this.currentRow);
      let colDiff = Math.abs(destinationCol - this.currentCol);

      // Move along a diagonal
      if (rowDiff === colDiff) {
        let rowStep = destinationRow > this.currentRow ? 1 : -1;
        let colStep = destinationCol > this.currentCol ? 1 : -1;

        let checkRow = this.currentRow + rowStep;
        let checkCol = this.currentCol + colStep;
        while (checkRow !== destinationRow && checkCol !== destinationCol) {
          if (this.board[checkRow][checkCol] !== null) {
            return false;
          }
          checkRow += rowStep;
          checkCol += colStep;
        }
      }

      // Move along a row or column
      if (
        destinationRow === this.currentRow ||
        destinationCol === this.currentCol
      ) {
        if (destinationRow === this.currentRow) {
          let start = Math.min(destinationCol, this.currentCol) + 1;
          let end = Math.max(destinationCol, this.currentCol);
          for (let col = start; col < end; col++) {
            if (this.board[destinationRow][col] !== null) {
              return false;
            }
          }
        } else {
          let start = Math.min(destinationRow, this.currentRow) + 1;
          let end = Math.max(destinationRow, this.currentRow);
          for (let row = start; row < end; row++) {
            if (this.board[row][destinationCol] !== null) {
              return false;
            }
          }
        }
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
