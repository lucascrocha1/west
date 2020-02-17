const fs = require('fs');

fs.copyFile('.\\src\\env\\env-dev.js', '.\\src\\env\\env.js', (err) => {
  if (err) throw err;
});