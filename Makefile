all:
	mcs SPP.cs Tokens/*.cs Parse/*.cs Tree/*.cs Special/*.cs
	make README.txt

clean:
	@rm -f *~ */*~

veryclean:
	@rm -f SPP.exe *~ */*~
	
README.txt:
	@echo "Madeline May and Jackie Robinson" > README.txt
