import pickle
import sklearn
import pandas as pd
import gensim
from gensim.utils import simple_preprocess
import nltk
from nltk.corpus import stopwords

nltk.download('punkt')
nltk.download('stopwords')
fillerWords = stopwords.words('english')
fillerWords.extend(["from","subject","re","edu","use"])

def preprocess (text):
  result = []
  for token in gensim.utils.simple_preprocess(text):
    if token not in gensim.parsing.preprocessing.STOPWORDS and len(token)>2 and token not in fillerWords:
      result.append(token)
  return result

def loadModel(fileName):
  with open(fileName,'rb') as f:
    model = pickle.load(f)
  return model

model = loadModel('fakeNews.pkl')
vect = loadModel('vectCount.pkl')

def predictFakeReal(text):
  data = {'text':[text]}
  dataset = pd.DataFrame(data = data)
  dataset['purgedText'] = dataset['text'].apply(preprocess)
  dataset['joinedPurgedText'] = dataset['purgedText'].apply(lambda x:" ".join(x))
  x = vect.transform(dataset['joinedPurgedText'])
  predictValue = model.predict(x)[0]
  if predictValue == 0:
    return 'Fake'
  return 'Real'

