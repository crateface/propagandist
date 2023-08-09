import spacy
from collections import Counter
from textblob import TextBlob

def getPolarity(text):
  polarity = TextBlob(text).sentiment.polarity
  return polarity

nlp = spacy.load("en_core_web_sm")

def findCountry(text):
  countries = []
  doc = nlp(text)
  for ent in doc.ents:
    if ent.label_ == "GPE":
      countries.append(ent.text)
  return countries

def mostCommon(text):
  countries = findCountry(text)
  if len(countries) == 0:
    return ""
  frequency = Counter(countries)
  return frequency.most_common(2)[0][0]

def getStateSupport(context):
  allies = ["us","united states","nato","eu","ukraine","poland"]
  enemies = ["russia", "china","csto","slovakia"]
  value = 1
  polarity = getPolarity(context)
  most_common_country = mostCommon(context)
  allies = [ally.lower() for ally in allies]
  enemies = [enemy.lower() for enemy in enemies]
  if most_common_country.lower() in allies:
    return polarity
  elif most_common_country.lower() in enemies:
    return -polarity
  return 0