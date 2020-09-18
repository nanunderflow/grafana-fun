require 'influxdb'

influxdb = InfluxDB::Client.new 'metrics'

data = {
  values: { value: 5 },
  tags: { operation: 'write' },
  timestamp: Time.now.to_i # timestamp is optional, if not provided point will be saved with current time
}

influxdb.write_point('foobar', data)

influxdb.query 'select * from foobar group by operation' do |name, tags, points|
  printf "%s\n", name
  points.each do |pt|
    printf "  -> %p\n", pt
  end
end
