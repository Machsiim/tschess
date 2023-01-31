export class Piece {
  constructor(currentRow, currentCol, color) {
    this.currentRow = currentRow;
    this.currentCol = currentCol;
    this.color = color;
  }

  isValidMove(destinationRow, destinationCol) {
    const rowDiff = Math.abs(destinationRow - this.currentRow);
    const colDiff = Math.abs(destinationCol - this.currentCol);

    if (
      destinationRow < 0 ||
      destinationRow > 7 ||
      destinationCol < 0 ||
      destinationCol > 7
    ) {
      return false;
    }

    if (
      destinationRow === this.currentRow &&
      destinationCol === this.currentCol
    ) {
      return false;
    }

    return true;
  }
}
