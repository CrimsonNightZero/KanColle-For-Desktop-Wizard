import requests
from bs4 import BeautifulSoup as bs
import os
import shutil
res=requests.get("http://threebards.com/kaini/full/")
soup=bs(res.text,"html.parser")
url=list(range(100))
url2=list(range(100))
i=0
Path="C:\\Users\\foryou\\Documents\\Visual Studio 2015\\Projects\\KanColleViewer\\KanColleViewer\\bin\\Debug\\Character"

for character in soup.select('a'):
	if "kaini" in character['href']:
		continue
	url[i]="http://threebards.com/kaini/full/"+character['href'];
	res2=requests.get(url[i])
	soup2=bs(res2.text,"html.parser")
	print(str(Path)+"\\"+str(character['href']))
	if False==os.path.isdir(Path+"\\"+character['href'].replace("\\","")):
		os.makedirs(r'%s/%s'%(Path,character['href']))
	for character2 in soup2.select('a'):
		if "kaini" in character2['href']:
			continue
		url2[i]=url[i]+character2['href'];
		res3=requests.get(url2[i],stream=True)
		##soup3=bs(res3.text,"html.parser")
		print(url2[i])
		name=character2['href']
		path2=Path+"\\"+character['href'].replace("/","\\")+"%s"
		print(str(path2)+","+name)
		f=open(path2 %(name),"wb")
		shutil.copyfileobj(res3.raw,f)
		print(str(character2['href']))
		
	i+=1
f.close()