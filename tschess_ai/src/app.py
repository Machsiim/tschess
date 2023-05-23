from flask import Flask, request, jsonify
import uuid
import chess
import chess.engine
import asyncio
from urllib.parse import *


app = Flask(__name__)

@app.route('/read/<fen>')
async def ai_move(fen):
    fen = request.view_args['fen']
    decoded_fen = unquote(fen)
    fen = decoded_fen.replace('_', '/')
    fen = fen.replace('=', ' ')
    _, engine = await chess.engine.popen_uci(['python', 'sunfish/sunfish/sunfish.py'])
    board = chess.Board(fen)
    result = await engine.play(board, chess.engine.Limit(time=0.1))
    board.push(result.move)
    await engine.quit()
    return jsonify({"fen": board.fen()})


@app.route('/encode')
def encode():
    fen = input("Enter the FEN string: ")
    converted_fen = fen.replace('/', '_')
    converted_fen = converted_fen.replace(' ', '=')
    return jsonify({"fen": converted_fen})


