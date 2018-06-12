createBoxPlot <- function(filenameInput, filenameOutput, chartName, testResult)
{
dat <- read.csv(file=filenameInput, header=FALSE, sep=",")

pdf(filenameOutput, encoding="ISOLatin2")

#boxplot(dat, col=c("blue", "red", "white","orange"), names=c("R","RVO","PR","R+"),  main=chartName, xlab="Algorithm name", ylab="Total time for all robots" )
#boxplot(dat, col=c("blue", "red", "white","orange"), names=c("R","RVO","PR","R+"),  main=chartName, xlab="Nazwa algorytmu", ylab="£¹ czny czas dla wszystkich robotów" )
boxplot(dat, col=c("blue", "red", "white","orange"), names=c("R","RVO","PR","R+"),  xlab="Algorithm", ylab="Total time for all robots"  )
#mtext(text=testResult, side=4, adj = 0, cex=0.7)

}

args = commandArgs(trailingOnly=TRUE)

if (length(args)==4) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	print("Chart Name: ")
	print(args[3])
	
	print("Test Result: ")
	print(args[4])

	createBoxPlot(args[1],args[2],args[3],args[4])
	dev.off()
} 
else 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


