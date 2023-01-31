export class Bishop extends Piece {
  constructor(currentRow, currentCol, color) {
    super(currentRow, currentCol, color);
  }
  isValidMove(destinationRow, destinationCol) {
    if (super.isValidMove(destinationRow, destinationCol)) {
      let rowDiff = Math.abs(destinationRow - this.currentRow);
      let colDiff = Math.abs(destinationCol - this.currentCol);

      if (rowDiff === colDiff) {
        let rowDirection = destinationRow > this.currentRow ? 1 : -1;
        let colDirection = destinationCol > this.currentCol ? 1 : -1;

        let checkRow = this.currentRow + rowDirection;
        let checkCol = this.currentCol + colDirection;
        while (checkRow !== destinationRow) {
          if (this.board[checkRow][checkCol] !== null) {
            return false;
          }
          checkRow += rowDirection;
          checkCol += colDirection;
        }

        if (
          this.board[destinationRow][destinationCol] === null ||
          this.board[destinationRow][destinationCol].color !== this.color
        ) {
          return true;
        }
      }
    }

    return false;
  }
}
