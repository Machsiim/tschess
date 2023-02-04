from flask import Flask, request, jsonify
import uuid

app = Flask(__name__)

@app.route("/api/game/new")
def create_game():
    return jsonify({"uuid": uuid.uuid4()})

@app.route("/api/game/<game_uuid>/move")
def move_chessman(game_uuid):
    game_uuid = request.view_args['game_uuid']
    move_from = request.args.get("from")
    move_to = request.args.get("to")
    if (not move_from): return "Invalid move from", 400
    if (not move_to): return "Invalid move to", 400
    return jsonify({"uuid": game_uuid, "from": move_from, "to": move_to})

@app.route("/api/game/<game_uuid>/finish")
def finish_game(game_uuid):
    return jsonify({"uuid": game_uuid});


