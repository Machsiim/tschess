export class Knight extends Piece {
  constructor(currentRow, currentCol, color) {
    super(currentRow, currentCol, color);
  }
  isValidMove(destinationRow, destinationCol) {
    if (super.isValidMove(destinationRow, destinationCol)) {
      let rowDiff = Math.abs(destinationRow - this.currentRow);
      let colDiff = Math.abs(destinationCol - this.currentCol);

      if (
        (rowDiff === 2 && colDiff === 1) ||
        (rowDiff === 1 && colDiff === 2)
      ) {
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
