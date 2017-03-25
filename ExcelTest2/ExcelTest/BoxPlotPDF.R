createBoxPlot <- function(filenameInput, filenameOutput)
{
dat <- read.csv(file=filenameInput, header=FALSE, sep=",")

pdf(filenameOutput)

boxplot(dat, col=c("blue", "green", "red", "white"), names=c("WR","CP","RVO","PD"))

}

dunn <- function(filename) {
	dat <- read.csv(file=filename, header=FALSE, sep=",")
	columns <- ncol(dat)
	rows <- nrow(dat)
	dat <- unname(unlist(dat, recursive=FALSE))

	ponds <- data.frame(pond=as.factor(rep(1:columns,each=rows)),
					res=dat)

	dunn.test(ponds$res,ponds$pond)
}

kw <- function(filename) {
	data <- read.csv(file=filename, header=FALSE, sep=",")
	columns <- ncol(data)
	rows <- nrow(data)
	data <- unname(unlist(data, recursive=FALSE))
	t <- c()
	for (i in 1:columns) {
  		t[i] <- rows
	}
	g <- factor(rep(1:columns, t))

	kruskal.test(data, g)
}


args = commandArgs(trailingOnly=TRUE)

if (length(args)==3) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	print("Tests output data file: ")
	print(args[3])

	createBoxPlot(args[1],args[2])
	dev.off()

	#library("dunn.test")

	#dunnResult <- dunn(args[1])
	#kwResult <- kw(args[1])	

	# Export test to file
	#sink(args[3])
	#library("dunn.test")
	#dunn(args[1])
	#cat("\n")
	#cat("---------------------------------------------------------")
	#cat("\n")
	#kw(args[1])
	#sink()

} else if (length(args)!=3) 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


