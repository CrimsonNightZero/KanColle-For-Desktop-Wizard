import os


def compare_text(end_text_temp,end_text_temp2):
	i = 0
	j = 0
	while i < 11:
		if end_text_temp in str(i) or end_text_temp in "x":
##			end_text=end_text_temp
			while j < 11:
				if end_text_temp2 in str(j) or end_text_temp2 in "x":
##					end_text2=end_text_temp2
					return end_text_temp, end_text_temp2
				j += 1
			return end_text_temp, ""
		i += 1
	return "", ""


def main(Path_first):
    with open(os.path.join(Path_first, 'en.txt'), 'r') as en_file:
        en = en_file.readline()
        while(en):
        	end_text_temp = en[len(en) - 2 : len(en) - 1]
        	end_text_temp2 = en[len(en) - 3 : len(en) - 2]
        	end_text, end_text2 = compare_text(end_text_temp, end_text_temp2)
        	#print(end_text)
        	
        	if end_text not in "" and end_text2 not in "":
        		text = en[0 : len(en) - 3] + "," + end_text + "," + end_text2
        	elif end_text not in "":
        		text = en[0 : len(en) - 2] + "," + end_text
        	else:
        		text = en.replace("\n", "")
        	if text in "chest":
        		break
        	
        
        	Path = "C:\\Users\\foryou\\Documents\\Visual Studio 2015\\Projects\\KanColleViewer\\KanColleViewer\\bin\\Debug\\Music"
        	abs_file_path = os.path.join(Path)
        	folders_temp = list(range(10000))
        	i = 0
        	x = True
        	for dirPaths, dirs, files in os.walk(abs_file_path):
        		for dir_name in dirs:
        			if dir_name in "Item發現":
        				x = False
        				
        				break;
        			folders_temp[i] = str(dir_name)
        			#f2.write(str(folders[i])+"\n")
        			i +=1
        		if x == False:
        			break
        			
        	folders = list(range(i))
        	j = 0
        	while i:
        		folders[j] = folders_temp[j]
        		j += 1
        		i -= 1
        	
        	i = 0
        	file_names_temp = list(range(10000))
        	for folder in folders:
        		j = 0
        		Path2 = os.path.join(Path, folder)
        		#f2.write(Path2+"\n")
        		abs_file_path2 = os.path.join(Path2)
        		
        		for dirPaths, dirs, files in os.walk(abs_file_path2):
        			for file in files:
        				if j == 0:
        					file_names_temp[i] = file.split("_")[0].split("0")[0] + "," + folder
        					i += 1
        				j = 1
        				if j == 0:
        					break
        		j += 1
                
        	file_names = list(range(i))
        	j = 0 
        	while i:
        		file_names[j] = file_names_temp[j]
        		j += 1
        		i -= 1
                
        with open(os.path.join(Path_first, 'ch.txt'), 'w', encoding = 'UTF-8') as ch_file:	
            x = False
            for file_name in file_names:
                #print(text.split(",")[0]+","+file_name.split(",")[0].lower())
            		if text.split(",")[0] in file_name.split(",")[0].lower():
            			ch_file.write(file_name.split(",")[1] + end_text2+end_text + "\n")
            			print(file_name.split(",")[1], end_text2, end_text)
            			x = True
            			break
            if x == False:
                ch_file.write(text.replace(",", "") + "\n")
        en = en_file.readline()
    	
    
if __name__ == '__main__':
    Path_first = r"C:\\Users\\foryou\\Documents\\Visual Studio 2015\\Projects\\KanColleViewer\\KanColleViewer\\bin\\Debug"
    main(Path_first)
    