max="$1"
date
echo "url: $2
rate: $max calls / second"
START=$(date +%s);

get () {
  curl -v "$1" 2>&1 | tr '\r\n' '\\n'
}

while true
do
  echo $(($(date +%s) - START)) | awk '{print int($1/60)":"int($1%60)}'
  sleep 1

  for i in `seq 1 $max`
  do
    get $2 &
  done
done
