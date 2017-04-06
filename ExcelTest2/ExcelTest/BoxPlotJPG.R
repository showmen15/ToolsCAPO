createBoxPlot <- function(filenameInput, filenameOutput)
{
dat <- read.csv(file=filenameInput, header=FALSE, sep=",")

jpeg(filenameOutput)

boxplot(dat, col=c("blue", "green", "red", "white","orange","yellow"), names=c("WR","CP","RVO","PD","WRNEW","CPNEW"))

}

args = commandArgs(trailingOnly=TRUE)

if (length(args)==2) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	createBoxPlot(args[1],args[2])
	dev.off()
} else if (length(args)!=2) 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


