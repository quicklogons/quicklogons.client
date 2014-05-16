<?php $settings = Settings::load(); ?>
<?php $callback_template = site_url(Settings::$callback_relative_url);?>
<div class="wrap">
    <h2>Quicklogons Settings</h2>
    <form method="post" action="options.php"> 
        <?php @settings_fields('quicklogons_plugin-group'); ?>
        <?php @do_settings_fields('quicklogons_plugin-group'); ?>

        <table class="form-table">
            <tr>
                <td colspan="2">
                    Please refer documentation to find out more about configuration: <a target="_blank" href="http://www.quicklogons.com/en-us/GetStarted">http://www.quicklogons.com/en-us/getstarted</a>
                </td>
            </tr>
            <tr valign="top">
                <th scope="row"><label for="quicklogons_callback">Callback</label></th>
                <td>
                    <textarea name="quicklogons_callback" id="quicklogons_callback" rows="5" style="width: 300px;" readonly=""><?php echo htmlspecialchars($callback_template); ?></textarea>
                    <p class="description">Copy this callback url template and paste into yours site settings on www.quicklogons.com</p>
                </td>
            </tr>
            <tr valign="top">
                <th scope="row"><label for="quicklogons_site_key">Site Key</label></th>
                <td>
                    <input style="width: 300px;" type="text" name="quicklogons_site_key" id="quicklogons_site_key" value="<?php echo $settings->site_key; ?>" />
                    <p class="description">Put here 'Site Key' from yours site settings on www.quicklogons.com</p>
                </td>
            </tr>
            <tr valign="top">
                <th scope="row"><label for="quicklogons_site_secret">Site Secret</label></th>
                <td>
                    <input style="width: 300px;" type="text" name="quicklogons_site_secret" id="quicklogons_site_secret" value="<?php echo $settings->site_secret; ?>" />
                    <p class="description">Put here 'Site Secret' from yours site settings on www.quicklogons.com</p>
                </td>
            </tr>
            <tr valign="top">
                <th scope="row"><label for="quicklogons_hash_algorithm">Hash Algorithm</label></th>
                <td>
                    <select style="width: 300px;" name="quicklogons_hash_algorithm" id="quicklogons_hash_algorithm">
                        <?php foreach(LandingProvider::$hash_algos as $key => $value):?>
                        <option value="<?php echo htmlspecialchars($key);?>" <?php if($key == $settings->hash_algorithm) echo "selected";?>><?php echo htmlspecialchars(strtoupper($key));?></option>
                        <?php endforeach;?>
                    </select>
                    <p class="description">Select the same hash algorithm as you have selected in yours site settings on www.quicklogons.com</p>
                </td>
            </tr>
        </table>

        <?php @submit_button(); ?>
    </form>
</div>