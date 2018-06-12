createBoxPlot <- function(filenameInput, filenameOutput, chartName, testResult)
{
dat <- read.csv(file=filenameInput, header=FALSE, sep=",")

pdf(filenameOutput, encoding="ISOLatin2")

#boxplot(dat, col=c("blue", "green", "red", "white","orange","yellow"), names=c("R","PF","RVO","PR","R+","PF+"),  main=chartName, xlab="Algorithm name", ylab="Total time for all robots" )
#boxplot(dat, col=c("blue", "green", "red", "white","orange","yellow"), names=c("R","PF","RVO","PR","R+","PF+"),  main=chartName, xlab="Nazwa algorytmu", ylab="£¹ czny czas dla wszystkich robotów"  )
boxplot(dat, col=c("blue", "green", "red", "white","orange","yellow"), names=c("R","PF","RVO","PR","R+","PF+"),  xlab="Algorithm", ylab="Total time for all robots"  )
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


