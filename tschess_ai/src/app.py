from flask import Flask, request, jsonify
import uuid
import chess
import chess.engine
import asyncio
from urllib.parse import *


app = Flask(__name__)

@app.route('/read', methods=['POST'])
async def ai_move():
    fen = request.json.get('fen')
    _, engine = await chess.engine.popen_uci(['python', 'sunfish/sunfish/sunfish.py'])
    board = chess.Board(fen)
    result = await engine.play(board, chess.engine.Limit(time=5.0))
    board.push(result.move)
    await engine.quit()
    return jsonify({"fen": board.fen()})








