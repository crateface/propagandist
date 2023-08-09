from flask import Flask, jsonify, request
from credibility import predictFakeReal
from statesupport import getStateSupport
from statesupport import getPolarity



TrueFalse = ""
support = 0.0
polarity = 0.0

app = Flask(__name__)
@app.route("/", methods = ['POST', 'GET'])

def getData():
	global TrueFalse
	global support
	global polarity
	if request.method == 'POST':
		data = request.form ["text"]
		TrueFalse = getTrueFalse(data)
		support = getSupport(data)
		polarity = getPolarityy(data)
		print("hello 2")
		return jsonify(data)
	else:
		data = {"TrueFalse":TrueFalse,"stateSupport":support,"polarity": polarity}
		print(data)
		return jsonify(data)


def sometign(streeng):
	return streeng + "extra"

def getTrueFalse(title):
	return predictFakeReal(title)

def getSupport(title):
	return getStateSupport(title)

def getPolarityy(title):
	print("heli")
	return getPolarity(title)
	

if __name__ == '__main__':
	app.run()
