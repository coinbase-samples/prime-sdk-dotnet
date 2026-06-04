SPEC_URL ?= https://api.prime.coinbase.com/v1/openapi.yaml
SPEC_FILE := apiSpec/prime-public-api-spec.yaml

.PHONY: fetch-spec
fetch-spec:
	@mkdir -p apiSpec
	curl -fsSL "$(SPEC_URL)" -o "$(SPEC_FILE)"
