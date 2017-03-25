dunn <- function(filename) {
	dat <- read.csv(file=filename, header=FALSE, sep=",")
	columns <- ncol(dat)
	rows <- nrow(dat)
	dat <- unname(unlist(dat, recursive=FALSE))

	ponds <- data.frame(pond=as.factor(rep(1:columns,each=rows)),
					res=dat)

	dunn.test(ponds$res,ponds$pond)
}


args = commandArgs(trailingOnly=TRUE)

if (length(args)==2) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	library("dunn.test")

	# Export test to file
	sink(args[2])
	print(dunn(args[1]))
	sink()

} else if (length(args)!=2) 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


