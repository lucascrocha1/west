const fs = require('fs');

fs.copyFile('.\\src\\env\\env-prod.js', '.\\src\\env\\env.js', (err) => {
  if (err) throw err;
});