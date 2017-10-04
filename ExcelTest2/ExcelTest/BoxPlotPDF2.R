createBoxPlot <- function(filenameInput, filenameOutput, chartName)
{
dat <- read.csv(file=filenameInput, header=FALSE, sep=",")

pdf(filenameOutput)

boxplot(dat, col=c("blue", "green", "red", "white","orange","yellow"), names=c("R","PF","RVO","PR","R+","PF+"),  main=chartName, xlab="Algorithm name", ylab="Total time for all robots" )

}

args = commandArgs(trailingOnly=TRUE)

if (length(args)==3) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	print("Chart Name: ")
	print(args[3])

	createBoxPlot(args[1],args[2],args[3])
	dev.off()
} else if (length(args)!=3) 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


