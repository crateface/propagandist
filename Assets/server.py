from flask import Flask, jsonify, request
from credibility import predictFakeReal
from statesupport import getStateSupport



TrueFalse = ""
support = 0

app = Flask(__name__)
@app.route("/", methods = ['POST', 'GET'])

def getData():
	global TrueFalse
	if request.method == 'POST':
		data = request.form ["text"]
		TrueFalse = getTrueFalse(data)
		support = getSupport(data)
		return jsonify(data)
	else:
		data = [{"TrueFalse":TrueFalse,"support":support}]
		print(data)
		return jsonify(data)


def sometign(streeng):
	return streeng + "extra"

def getTrueFalse(title):
	return predictFakeReal(title)

def getSupport(title):
	return getStateSupport(title)

if __name__ == '__main__':
	app.run()
