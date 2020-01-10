import os


def main(Path):
     with open(os.path.join(Path, 'en.txt'), 'w', encoding='UTF-8') as f:
        folder = list(range(10000))
        folder_index = 0
        for dirPath, dirs, files in os.walk(Path):
            	for dir_name in dirs:
            		if dir_name not in "Music":
            			folder[folder_index] = dir_name
            			print(dir_name)
            			folder_index += 1
        			
        folder_index = 0
        i = 2
        while folder_index < 2:
            	print(str(folder[folder_index]), str(folder[i]))
            	abs_file_path = os.ppath.join(Path , folder[folder_index] , folder[i])
            	f.write(folder[folder_index] + "\n")
                
            	for dirPath ,dirs ,files in os.walk(abs_file_path):
            		for file in files:
            			f.write(file.split('.')[0] + "\n")
            			##print(file)
            	if folder[i] in "SS" or folder[i] in "window":
            		folder_index += 1
            		i += 1
            	else:
            		i += 1          
                    
                    
if __name__ == '__main__':
    
    Path = r"C:\\Users\\foryou\\Documents\\Visual Studio 2015\\Projects\\KanColleViewer\\KanColleViewer\\bin\\Debug"
    main(Path)
                                                                                                          				