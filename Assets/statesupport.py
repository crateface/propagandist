import spacy
from collections import Counter
from textblob import TextBlob

def getPolarity(text):
  polarity = TextBlob(text).sentiment.polarity
  return polarity

print(getPolarity(""))
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
  frequency = Counter(countries)
  return frequency.most_common(2)[-1][0]

def getStateSupport(context, country, enemies, allies):
  value = 2
  polarity = get_polarity(context)
  most_common_country = get_most_freq_country(context)
  allies = [ally.lower() for ally in allies]
  enemies = [enemy.lower() for enemy in enemies]
  if most_common_country.lower() == country.lower() or most_common_country.lower() in allies:
    if polarity > 0:
      return value
    elif polarity < 0:
      return -value
  elif most_common_country.lower() in enemies:
    if polarity > 0:
      return -value
    elif polarity < 0:
      return value
  return 0