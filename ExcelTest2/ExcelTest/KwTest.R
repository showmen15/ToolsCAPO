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

if (length(args)==2) 
{
	print("Input data file: ")
	print(args[1])

	print("Output data file: ")
	print(args[2])

	# Export test to file
	sink(args[2])
	print(kw(args[1]))
	sink()

} else if (length(args)!=2) 
{
	stop("Podaj plik wejsciowy oraz wyjsciowy", call.=FALSE)
}


