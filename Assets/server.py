from flask import Flask, jsonify, request

output = ""

app = Flask(__name__)
@app.route("/", methods = ['POST', 'GET'])

def getData():
	global output
	if request.method == 'POST':
		data = request.form ["text"]
		output = sometign(data)
		return jsonify(data)
	else:
		data = output
		return jsonify(data)


def sometign(streeng):
	return streeng + "extra"

if __name__ == '__main__':
	app.run()
